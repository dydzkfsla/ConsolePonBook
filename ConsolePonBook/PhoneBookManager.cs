using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

    class PhoneBookManager 
    {
        static PhoneBookManager bookManager;
        PhoneInfo[] phones = new PhoneInfo[MAX_CNT];
        HostComparer Com = new HostComparer();
        int curCnt = 7;
        const int MAX_CNT = 100;

        private PhoneBookManager()
        {

        }

        public static PhoneBookManager Create()
        {
            if (bookManager == null)
                bookManager = new PhoneBookManager();

            return bookManager;
        }

        //tring name;            //필수
        //string phoneNumber;     //필수
        //string birth;           //선택

        public void ShowMenu()
        {
            phones[0] = new PhoneInfo("머하니", "01084851561", "1996");
            phones[1] = new PhoneInfo("김김김", "01021845561", "1997");
            phones[2] = new PhoneInfo("받받박", "01015615561");
            phones[3] = new PhoneUnivInfo("용칼님", "01015615561", "스마트it","1745");
            phones[4] = new PhoneUnivInfo("용할님", "0101225561","1561","메카트로닉", "1845");
            phones[5] = new PhoneCompanyInfo("용님", "01045615561", "1161", "(주)하느님");
            phones[6] = new PhoneCompanyInfo("양님", "01084615561", "(주)부처님");
            Console.WriteLine("----------------------------------- 주소록 ------------------------------------");
            Console.WriteLine("1. 입력  |  2. 목록  | 3. 검색 | 4. 정렬  |  5. 삭제  | 6. 종료 |");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.Write("선택 : ");
        }

        #region 외부 사용 메서드 
        public void InputData()
        {
            while (curCnt != 100)
            {
                Regex temp = new Regex(@"[1-4]{1}$");
                Console.Clear();
                Console.WriteLine($"{curCnt + 1}번째 사용자의 정보를 입력해야 합니다.");
                Console.WriteLine("1.일반 지인 입력    2.회사 지인 입력   3.학교 지인 입력 4.나가기");
                Console.Write("선택: ");
                string cho = CaseInput(temp);
                if (cho == "-1") continue;
                switch (int.Parse(cho))     //각 타입에 맞게 변환후 입력
                {
                    case 1: PhoneData(ref phones[curCnt++]); break;
                    case 2: phones[curCnt] = new PhoneCompanyInfo(); PhoneData(ref phones[curCnt++]); break;
                    case 3: phones[curCnt] = new PhoneUnivInfo(); PhoneData(ref phones[curCnt++]); break;
                    case 4: Console.Clear();return;
                }
            }
        }
        public void ListData()
        {
            if(curCnt == 0)
            {
                Console.WriteLine("입력된 것이 없습니다.");
                System.Threading.Thread.Sleep(1000);        //강제 대기 
                //Console.ReadLine();       사용자가 확인 하면다음으로
                return;
            }

            for(int i =0; i < curCnt; i++)
            {
                Console.WriteLine($"{phones[i].ToString()}");        //생일의 초기값 = "Not data"
            }
            Console.ReadLine();
        }
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
        public void DeleteData()        //딜리트는 실수로 한다면 문제가 많기 때문에 여러번 할 수 없게 만든다
        {
            int cho = -1;
            int temp = -1;
            Console.Clear();
            Console.WriteLine("목록");
            for (int i = 0; i < curCnt; i++)
            {
                Console.WriteLine($"{i + 1}번째 사용자 폰 넘버: {phones[i].PhoneNumber}");//중복되지 않는 고유의 값 폰 넘버
            }
            Console.WriteLine("지우고 싶은 번호를 입력하세요");

            Console.Write("선택 : ");
            try                                                 //묶고싶음 하지만 귀찮다.
            {
                cho = int.Parse(Console.ReadLine());            //TrtParse의 경우 스택 오버플로우위 오류 생성
                if (cho > curCnt || cho < 0) throw new Exception();
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

            for (int i = cho; i < curCnt - 1; i++)     // 55를 선택할경우 cho = 54
            {
                phones[i] = phones[i + 1];          //값을 앞으로 당김 큐
            }
            phones[curCnt--] = null;        //앞으로 당긴후 마지막을 null로 후 에 카운트 -1
        }
        public void Compar()
        {
            Console.Clear();
            Regex temp = new Regex(@"[1-6]{1}$");
            PhoneInfo[] potemp = new PhoneInfo[curCnt];
            Console.WriteLine("정렬할 기준을 선택하세요");
            Console.WriteLine("1.이름\t2.폰번호\t3.생일\t4.전공\t5.입학년도\t6.회사");
            int cho =int.Parse(CaseInput(temp));
            switch (cho)
            {
                case 1: Com = new NameComparer(); Array.Copy(phones, potemp, curCnt); Array.Sort(phones, Com); break;
                case 2: Com = new PhoneNumberComparer(); Array.Copy(phones, potemp, curCnt); Array.Sort(potemp, Com); break;
                case 3: Com = new BirthComparer(); Array.Copy(phones, potemp, curCnt); Array.Sort(potemp, Com); break;
                case 4: Com = new MajorComparer(); Array.Copy(phones, potemp, curCnt); Array.Sort(potemp, Com); break;
                case 5: Com = new YearComparer(); Array.Copy(phones, potemp, curCnt); Array.Sort(potemp, Com); break;
                case 6: Com = new CompanyComparer(); Array.Copy(phones, potemp, curCnt); Array.Sort(potemp, Com); break;

                default: Console.WriteLine("잘못되었습니다."); return;
            }

            for (int i = 0; i < curCnt; i++)
            {
                Console.WriteLine($"{potemp[i].ToString()}");        //생일의 초기값 = "Not data"
            }
            Console.ReadLine();

        }
        #endregion

        #region 내부 사용 메서드
        private void SearchForName()
        {
            Console.Clear();
            Console.Write("검색할 이름을 입력하세요: ");
            string name = Console.ReadLine();
            Console.WriteLine("검색결과 ");
            for (int i = 0; i < curCnt; i++)
            {
                if(phones[i].Name == name)
                {
                    Console.WriteLine($"{phones[i].Name}씨 폰번호 : {phones[i].PhoneNumber} 생일 : {phones[i].Birth}");
                }
            }
            Console.ReadLine();
        }
        private void SearchForBirth()
        {
            Console.Clear();
            Console.Write("검색할 생일을 입력하세요: ");
            string Birth = Console.ReadLine();
            Console.WriteLine("검색결과 ");
            for (int i = 0; i < curCnt; i++)
            {
                if (phones[i].Birth == Birth)
                {
                    Console.WriteLine($"{phones[i].Name}씨 폰번호 : {phones[i].PhoneNumber} 생일 : {phones[i].Birth}");
                }
            }
            Console.ReadLine();
        }
        private void SearchForPhoneNumber()
        {
            Console.Clear();
            Console.Write("검색할 폰번호을 입력하세요: ");
            string PhoneNumber = Console.ReadLine();
            Console.WriteLine("검색결과 ");
            for (int i = 0; i < curCnt; i++)
            {
                if (phones[i].PhoneNumber == PhoneNumber)
                {
                    Console.WriteLine($"{phones[i].Name}씨 폰번호 : {phones[i].PhoneNumber} 생일 : {phones[i].Birth}");
                }
            }
            Console.ReadLine();
        }
        private void PhoneData(ref PhoneInfo phoneinfo)
        {
            Console.Clear();
            Regex temp = new Regex(@"^[가-힣]{1,}$"); //이름입력 조건은 1개이상 한글
            Console.Write("이름: ");
            string name = CaseInput(temp);
            if (name == "-1") { ShowErr(); phoneinfo = null; return; }

            temp = new Regex(@"^01[01678][0-9]{4}[0-9]{4}$");   //폰입력조건
            Console.Write("폰번호(- 제외): ");
            string phoneNumber = CaseInput(temp);       
            if (phoneNumber == "-1") { ShowErr(); phoneinfo = null; return; }

            temp = new Regex(@"^[\d]{1,}$");
            Console.Write("생일입력: ");
            string birth = CaseInput(temp);

            if (phoneinfo is PhoneUnivInfo)  //회사 형태의 Phoneinof면
            {
                temp = new Regex(@"^[\D]{1,}$");
                Console.Write("전공입력: ");
                string major = CaseInput(temp);
                if (major == "-1") { ShowErr(); phoneinfo = null; return; }

                temp = new Regex(@"^[\d]{4}$");
                Console.Write("년도입력: ");
                string year = CaseInput(temp);
                if (year == "-1") { ShowErr(); phoneinfo = null; return; }

                if (birth == "-1")
                {
                    phoneinfo = new PhoneUnivInfo(name, phoneNumber, major, year);
                    return;         //값이 할당 됬으므로
                }
                else
                {
                    phoneinfo = new PhoneUnivInfo(name, phoneNumber, birth, major, year);
                    return;
                }
            }
            else if(phoneinfo is PhoneCompanyInfo)
            {
                temp = new Regex(@"^[\D]{1,}$");
                Console.Write("회사입력: ");
                string company = CaseInput(temp);
                if (company == "-1") { ShowErr(); return; }

                if (birth == "-1")
                {
                    phoneinfo = new PhoneCompanyInfo(name, phoneNumber, company);
                    return;
                }
                else 
                { 
                    phoneinfo = new PhoneCompanyInfo(name, phoneNumber, birth, company);
                    return;
                }
            }
            else
            {
                if (birth == "-1")
                {
                    phoneinfo = new PhoneInfo(name, phoneNumber);
                    return;
                }
                else
                {
                    phoneinfo = new PhoneInfo(name, phoneNumber, birth);
                    return;
                }
            }
        }
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
        private void ShowErr()
        {
            curCnt--;
            Console.WriteLine("입력이 잘못되었습니다.");
            Console.ReadLine();
        }
        #endregion

    }
}
