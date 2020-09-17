using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePonBook
{
    class PhoneBookManager
    {
        PhoneInfo[] phones = new PhoneInfo[MAX_CNT];
        int curCnt = 0;
        const int MAX_CNT = 100;

        //tring name;            //필수
        //string phoneNumber;     //필수
        //string birth;           //선택

        public void ShowMenu()
        {
            Console.WriteLine("----------------------- 주소록 --------------------------");
            Console.WriteLine("1. 입력  |  2. 목록  | 3. 검색  |   4. 삭제  | 5. 종료 |");
            Console.WriteLine("---------------------------------------------------------");
            Console.Write("선택 : ");
        }

        public void InputData()
        {
            while (curCnt != 100)
            {
                int cho = 0;
                Console.Clear();
                Console.WriteLine($"{curCnt + 1}번째 사용자의 정보를 입력해야 합니다.");
                Console.WriteLine("1.생일을 제외한 입력    2.생일을 포함한 입력   3.처음으로");
                Console.Write("선택: ");
                try
                {
                    cho = int.Parse(Console.ReadLine());            //TrtParse의 경우 스택 오버플로우위 오류 생성
                    if (cho > 3 || cho < 0) throw new Exception();
                }catch(Exception e)
                {
                    Console.WriteLine("입력이 잘못되었습니다.");
                    Console.ReadLine();
                    Console.Clear();
                    continue;           //해당메소드를 처음부터
                }

                switch (cho) 
                {
                    case 1: NoBirthInputData(); break;
                    case 2: YesBirthInputData(); break;
                    case 3: Console.Clear();return;
                }
            }
        }
        private void NoBirthInputData()
        {
            Console.Clear();
            Console.Write("이름: ");
            string name = Console.ReadLine();
            Console.Write("폰번호: ");
            string phoneNumber = Console.ReadLine();        //만약 정규식 사용이 가능하다면
            phones[curCnt++] = new PhoneInfo(name, phoneNumber);
        }
        private void YesBirthInputData()
        {
            Console.Clear();
            Console.Write("이름: ");
            string name = Console.ReadLine();
            Console.Write("폰번호: ");
            string phoneNumber = Console.ReadLine();        //만약 정규식 사용이 가능하다면
            Console.Write("생일: ");
            string birth = Console.ReadLine();        //만약 정규식 사용이 가능하다면
            phones[curCnt++] = new PhoneInfo(name, phoneNumber, birth);
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
                Console.WriteLine($"{i + 1}번째: {phones[i].Name}씨 폰번호 : {phones[i].PhoneNumber} 생일 : {phones[i].Birth}");        //생일의 초기값 = "Not data"
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
            catch (Exception e)
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

        public void DeleteData()        //딜리트는 실수로 한다면 문제가 많기 때문에 여러번 할 수 없게 만든다
        {
            int cho = -1;
            int temp = -1;
            Console.Clear();
            Console.WriteLine("목록");
            for(int i=0; i < curCnt; i++)
            {
                Console.WriteLine($"{i+1}번째 사용자 폰 넘버: {phones[i].PhoneNumber}");//중복되지 않는 고유의 값 폰 넘버
            }
            Console.WriteLine("지우고 싶은 번호를 입력하세요");

            Console.Write("선택 : ");
            try                                                 //묶고싶음 하지만 귀찮다.
            {
                cho = int.Parse(Console.ReadLine());            //TrtParse의 경우 스택 오버플로우위 오류 생성
                if (cho > curCnt || cho < 0) throw new Exception();
            }catch (Exception e)
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
            catch (Exception e)
            {
                Console.WriteLine("입력이 잘못되었습니다.");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            if (temp == 2) return;

            for(int i = cho; i < curCnt-1; i++)     // 55를 선택할경우 cho = 54
            {
                phones[i] = phones[i + 1];          //값을 앞으로 당김 큐
            }
            phones[curCnt--] = null;        //앞으로 당긴후 마지막을 null로 후 에 카운트 -1
            

        }
    }
}
