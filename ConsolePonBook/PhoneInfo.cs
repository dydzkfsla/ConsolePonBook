using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePonBook
{
    class PhoneInfo
    {
        string name;            //필수
        string phoneNumber;     //필수
        string birth = "Not data";           //선택


        public PhoneInfo(string name, string phoneNumber)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
        }

        public PhoneInfo(string name, string phoneNumber,string birth)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.birth = birth;
        }

        public string Name { get => name;}
        public string PhoneNumber { get => phoneNumber;}
        public string Birth { get => birth;}

        public void ShowPhoneNumber()
        {

        }


    }
}
