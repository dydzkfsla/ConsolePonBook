using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsolePonBook
{
    class InputException : System.Exception
    {
        public InputException() : base() { }
        public InputException(string message) : base(message) { }
        public InputException(string message, System.Exception inner) : base(message, inner) { }

        protected InputException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// 전체적인 부분 ui를 보여주며 해당 데이터를 가지고 주 기능을 담당함
    /// </summary>
    class PhoneBookManager
    {
        //입력시 무엇을 선택했는지 확인
        enum PhonesChoose { phoneInfo = 1, phoneUnivInfo = 2, phoneCompanyInfo = 3 }
        //파일 이름
        readonly string dataFile = "PhooneBookManager.dat";
        //폰북 매니저 싱글턴
        static PhoneBookManager bookManager;
        //주 데이터의 배열
        HashSet<PhoneInfo> phones = new HashSet<PhoneInfo>();
        //정렬기준점
        HostComparer Com = new HostComparer();
        
        //싱글턴
        private PhoneBookManager()
        {
            try
            {
                BinaryFormatter binary = new BinaryFormatter();
                using (FileStream file = new FileStream(dataFile, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    if (!(file.Length == 0))
                    {
                        phones = (HashSet<PhoneInfo>)binary.Deserialize(file);
                        file.Flush();
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        //생성
        public static PhoneBookManager Create()
        {
            if (bookManager == null)
                bookManager = new PhoneBookManager();

            return bookManager;
        }

        //tring name;            //필수
        //string phoneNumber;     //필수
        //string birth;           //선택

        //매뉴를 보여줌
        public void ShowMenu()
        {
            Console.WriteLine("----------------------------------- 주소록 ------------------------------------");
            Console.WriteLine("1. 입력  |  2. 목록  | 3. 검색 | 4. 정렬  |  5. 삭제  | 6. 종료 |");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.Write("선택 : ");
        }

        #region 외부 사용 메서드 
        //입력 데이터
        public void InputData()
        {
            while (true)
            {
                Regex temp = new Regex(@"[1-4]{1}$");
                Console.Clear();
                Console.WriteLine($"{phones.Count+1}번째 사용자의 정보를 입력해야 합니다.");
                Console.WriteLine("1.일반 지인 입력    2.회사 지인 입력   3.학교 지인 입력 4.나가기");
                Console.Write("선택: ");
                string cho = CaseInput(temp);
                if (cho == "-1") continue;
                switch (int.Parse(cho))     //각 타입에 맞게 변환후 입력
                {
                    case 1: PhoneData(PhonesChoose.phoneInfo); break;
                    case 2: PhoneData(PhonesChoose.phoneCompanyInfo); break;
                    case 3: PhoneData(PhonesChoose.phoneUnivInfo); break;
                    case 4: Console.Clear(); return;
                }
            }
        }
        //데이터 보여줌
        public void ListData()
        {
            if (phones.Count == 0)
            {
                Console.WriteLine("입력된 것이 없습니다.");
                System.Threading.Thread.Sleep(1000);        //강제 대기 
                //Console.ReadLine();       사용자가 확인 하면다음으로
                return;
            }

            foreach(PhoneInfo phone in phones) {
                Console.WriteLine($"{phone.ToString()}");        //생일의 초기값 = "Not data"
            }
            Console.ReadLine();
        }
        //검색 데이터
        public void SearchData()
        {
            Console.Clear();
            int temp = -1;
            Console.WriteLine("검색할 기준을 고르세요");
            Console.WriteLine("1. 이름  2.폰번호  3.생일");

            try
            {
                temp = int.Parse(Console.ReadLine());            //TrtParse의 경우 스택 오버플로우위 오류 생성
                if (temp > 3 || temp < 0) throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("입력이 잘못되었습니다.");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            Console.Clear();
            switch (temp)
            {
                case 1: SearchForName(); break;
                case 2: SearchForPhoneNumber(); break;
                case 3: SearchForBirth(); break;
            }
            Console.Clear();
        }
        //데이터 삭제
        public void DeleteData()        //딜리트는 실수로 한다면 문제가 많기 때문에 여러번 할 수 없게 만든다
        {
            int cho = -1;
            int temp = -1;
            int count = 1;
            Console.Clear();
            Console.WriteLine("목록");
            foreach(PhoneInfo phone in phones) {
                Console.WriteLine($"{count++}번째 사용자 폰 넘버: {phone.PhoneNumber}");//중복되지 않는 고유의 값 폰 넘버
            }
            Console.WriteLine("지우고 싶은 번호를 입력하세요");

            Console.Write("선택 : ");
            try                                                 //묶고싶음 하지만 귀찮다.
            {
                cho = int.Parse(Console.ReadLine());            //TrtParse의 경우 스택 오버플로우위 오류 생성
                if (cho > count || cho < 0) throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("입력이 잘못되었습니다.");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            cho--; //사용자의 입력값이 100일경우 99의 배열을 선택한 것이다.

            Console.WriteLine("정말로 삭제 하시겠습니까? 1. 예 2.아니오");
            try
            {
                temp = int.Parse(Console.ReadLine());            //TrtParse의 경우 스택 오버플로우위 오류 생성
                if (temp > 2 || temp < 0) throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("입력이 잘못되었습니다.");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            if (temp == 2) return;

            phones.Remove(phones.ElementAt(cho));
        }
        //정렬및 기준 확인
        public void Compar()
        {
            Console.Clear();
            Regex temp = new Regex(@"[1-6]{1}$");
            //List<PhoneInfo> list = phones.ToList();
            List<PhoneInfo> list = new List<PhoneInfo>(phones);
            Console.WriteLine("정렬할 기준을 선택하세요");
            Console.WriteLine("1.이름\t2.폰번호\t3.생일\t4.전공\t5.입학년도\t6.회사");
            int cho = int.Parse(CaseInput(temp));
            switch (cho)
            {
                case 1: Com = new NameComparer(); list.Sort(Com); break;
                case 2: Com = new PhoneNumberComparer(); list.Sort(Com); break;
                case 3: Com = new BirthComparer(); list.Sort(Com); break;
                case 4: Com = new MajorComparer(); list.Sort(Com); break;
                case 5: Com = new YearComparer(); list.Sort(Com); break;
                case 6: Com = new CompanyComparer(); list.Sort(Com); break;

                default: Console.WriteLine("잘못되었습니다."); return;
            }

            foreach(PhoneInfo phoneInfo in list) {
                Console.WriteLine($"{phoneInfo}");        //생일의 초기값 = "Not data"
            }
            Console.ReadLine();

        }
        //끝날때 하는 동작 
        public void END()
        {
            try
            {
                BinaryFormatter binary = new BinaryFormatter();
                using (FileStream file = new FileStream(dataFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    binary.Serialize(file, phones);
                    file.Flush();
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 내부 사용 메서드
        //검색 이름기준
        private void SearchForName()
        {
            Console.Clear();
            Console.Write("검색할 이름을 입력하세요: ");
            string name = Console.ReadLine();
            Console.WriteLine("검색결과 ");
            foreach (PhoneInfo phone in phones)
            {
                if (phone.Name == name)
                {
                    Console.WriteLine($"{phone.Name}씨 폰번호 : {phone.PhoneNumber} 생일 : {phone.Birth}");
                }
            }
            Console.ReadLine();
        }
        //검색 생일기준
        private void SearchForBirth()
        {
            Console.Clear();
            Console.Write("검색할 생일을 입력하세요: ");
            string Birth = Console.ReadLine();
            Console.WriteLine("검색결과 ");
            foreach (PhoneInfo phone in phones)
            {
                if (phone.Birth == Birth)
                {
                    Console.WriteLine($"{phone.Name}씨 폰번호 : {phone.PhoneNumber} 생일 : {phone.Birth}");
                }
            }
            Console.ReadLine();
        }
        //검색 폰넘버기준
        private void SearchForPhoneNumber()
        {
            Console.Clear();
            Console.Write("검색할 폰번호을 입력하세요: ");
            string PhoneNumber = Console.ReadLine();
            Console.WriteLine("검색결과 ");
            foreach(PhoneInfo phone in phones)
            {
                if (phone.PhoneNumber == PhoneNumber)
                {
                    Console.WriteLine($"{phone.Name}씨 폰번호 : {phone.PhoneNumber} 생일 : {phone.Birth}");
                }
            }
            Console.ReadLine();
        }
        //검색 데이터 입력
        private void PhoneData(PhonesChoose choose)
        {
            Console.Clear();

            bool Add = false;
            Regex temp = new Regex(@"^[가-힣]{1,}$"); //이름입력 조건은 1개이상 한글
            Console.Write("이름: ");
            string name = CaseInput(temp);
            if (name == "-1") { ShowErr(); return; }

            temp = new Regex(@"^01[01678][0-9]{4}[0-9]{4}$");   //폰입력조건
            Console.Write("폰번호(- 제외): ");
            string phoneNumber = CaseInput(temp);
            if (phoneNumber == "-1") { ShowErr();return; }

            temp = new Regex(@"^[\d]{1,}$");
            Console.Write("생일입력: ");
            string birth = Console.ReadLine().Trim();
            if (!temp.IsMatch(birth))
                birth = "-1";

            if (choose == PhonesChoose.phoneUnivInfo)  //회사 형태의 Phoneinof면
            {
                temp = new Regex(@"^[\D]{1,}$");
                Console.Write("전공입력: ");
                string major = CaseInput(temp);
                if (major == "-1") {ShowErr(); return;}

                temp = new Regex(@"^[\d]{4}$");
                Console.Write("년도입력: ");
                string year = CaseInput(temp);
                if (year == "-1") { ShowErr(); return; }

                if (birth == "-1")
                    Add = phones.Add(new PhoneUnivInfo(name, phoneNumber, major, year));
                else
                    Add = phones.Add(new PhoneUnivInfo(name, phoneNumber, birth, major, year));
            }
            else if (choose == PhonesChoose.phoneCompanyInfo)
            {
                temp = new Regex(@"^[\D]{1,}$");
                Console.Write("회사입력: ");
                string company = CaseInput(temp);
                if (company == "-1") { ShowErr(); return; }

                if (birth == "-1")
                    Add = phones.Add(new PhoneCompanyInfo(name, phoneNumber, company));
                else
                    Add = phones.Add(new PhoneCompanyInfo(name, phoneNumber, birth, company));
            }
            else
            {
                if (birth == "-1")
                    Add = phones.Add(new PhoneInfo(name, phoneNumber));
                else
                    Add = phones.Add(new PhoneInfo(name, phoneNumber, birth));
            }
            if (Add)
                Console.WriteLine("입력되었습니다.");
            else
                Console.WriteLine("중복된 번호가 있습니다.");
            Console.ReadLine();
        }
        //검색 입력시 입력값 체크
        private string CaseInput(Regex temp)
        {
            try
            {
                string cho = Console.ReadLine().Trim();            //TrtParse의 경우 스택 오버플로우위 오류 생성
                if (temp.IsMatch(cho))
                {
                    return cho;
                }
                else
                    throw new InputException("입력 오류입니다.");
            }
            catch (InputException err)
            {
                Console.WriteLine(err.Message);
                Console.ReadLine();
                return "-1";
            }
        }
        //검색 오류 확인
        private void ShowErr()
        {
            Console.WriteLine("입력이 잘못되었습니다.");
            Console.ReadLine();
        }
        #endregion

    }
}
