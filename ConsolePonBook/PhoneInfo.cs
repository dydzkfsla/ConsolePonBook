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

        public override string ToString()
        {
            return $"name : {name}\tphoneNumber : {phoneNumber}\tbirth : {birth}";

        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

    }

    class PhoneUnivInfo : PhoneInfo
    {
        string major;
        string year;

        public PhoneUnivInfo(string name, string phoneNumber,string major, string year) : base(name, phoneNumber)
        {
            this.major = major;
            this.year = year;
        }

        public PhoneUnivInfo(string name, string phoneNumber, string birth, string major, string year) : base(name, phoneNumber, birth)
        {
            this.major = major;
            this.year = year;
        }

        public string Major { get => major; set => major = value; }
        public string Year { get => year; set => year = value; }

        public override string ToString()
        {
            return base.ToString() + $"\tmajor : {major}\tyear = {year}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    class PhoneCompanyInfo : PhoneInfo 
    {
        string company;

        public PhoneCompanyInfo(string name, string phoneNumber, string company) : base(name, phoneNumber)
        {
            this.company = company;
        }

        public PhoneCompanyInfo(string name, string phoneNumber, string birth, string company) : base(name, phoneNumber, birth)
        {
            this.company = company;
        }

        public string Company { get => company; set => company = value; }

        public override string ToString()
        {
            return base.ToString() + $"\tcompany : {company}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

}
