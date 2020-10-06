using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePonBook
{
    /// <summary>
    /// 해당 class가 무엇인가를 알려줄 인터페이스
    /// </summary>
    interface IUniv { };
    interface ICom { };

    /// <summary>
    /// 각 데이터가 가져야할 프로퍼티 및 데이터들
    /// </summary>
    interface IName { string Name { get; set; } }
    interface IPhoenNumber { string PhoneNumber { get; set; } }
    interface Ibirth { string Birth { get; set; } }
    interface Icompany { string Company { get; set; } }
    interface Imajor { string Major { get; set; } }
    interface Iyear { string Year { get; set; } }

    /// <summary>
    /// 실제 들어가 데이터의 class 
    /// </summary>
    [Serializable]
    class PhoneInfo : IName, IPhoenNumber, Ibirth
    {
        #region 내부 사용 맴버
        string name;            //필수
        string phoneNumber;     //필수
        string birth = "Not data";           //선택
        #endregion

        #region I프로퍼티
        public string Name { get => name; set => name = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Birth { get => birth; set => birth = value; }
        #endregion

        #region 생성자
        public PhoneInfo()
        {

        }

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
        #endregion


        #region 오버라이드 메서드
        public override string ToString()
        {
            return $"name : {name}\tphoneNumber : {phoneNumber}\tbirth : {birth}";
        }

        public override bool Equals(object obj)
        {
            return obj is PhoneInfo info && PhoneNumber == info.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return 1603624094 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
        }

        #endregion

    }

    [Serializable]
    class PhoneUnivInfo : PhoneInfo , IUniv, Imajor, Iyear
    {
        #region 내부 맴버
        string major;
        string year;
        #endregion

        #region I프로퍼티
        public string Major { get => major; set => major = value; }
        public string Year { get => year; set => year = value; }
        #endregion

        #region 생성자
        public PhoneUnivInfo()
        {

        }

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
        #endregion

        #region 오버라이드 메서드
        public override string ToString()
        {
            return base.ToString() + $"\tmajor : {major}\tyear = {year}";
        }

        public override bool Equals(object obj)
        {
            return obj is PhoneInfo info && base.PhoneNumber == info.PhoneNumber;
        }
        #endregion
    }

    [Serializable]
    class PhoneCompanyInfo : PhoneInfo , ICom, Icompany
    {
        #region 내부맴버
        string company;
        #endregion

        #region I프로퍼티 
        public string Company { get => company; set => company = value; }
        #endregion

        #region 생성자
        public PhoneCompanyInfo()
        {

        }

        public PhoneCompanyInfo(string name, string phoneNumber, string company) : base(name, phoneNumber)
        {
            this.company = company;
        }

        public PhoneCompanyInfo(string name, string phoneNumber, string birth, string company) : base(name, phoneNumber, birth)
        {
            this.company = company;
        }
        #endregion

        #region 오버라이드 메서드
        public override string ToString()
        {
            return base.ToString() + $"\tcompany : {company}";
        }

        public override bool Equals(object obj)
        {
            return obj is PhoneInfo info && PhoneNumber == info.PhoneNumber;
        }
        #endregion
    }

}
