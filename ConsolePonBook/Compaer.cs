using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePonBook
{
    class HostComparer : IComparer { public int Compare(object x, object y) { throw new NotImplementedException(); } };

    class NameComparer : HostComparer, IComparer
    {
        public int Compare(object x, object y)
        {
            PhoneInfo first = x as PhoneInfo;
            PhoneInfo secend = y as PhoneInfo;

            if (first.Name.CompareTo(secend.Name) == 1) return 1;
            else if (first.Name.CompareTo(secend.Name) == -1) return -1;
            else return 0;
        }
    }

    class PhoneNumberComparer : HostComparer, IComparer
    {
        public int Compare(object x, object y)
        {
            PhoneInfo first = x as PhoneInfo;
            PhoneInfo secend = y as PhoneInfo;

            if (first.PhoneNumber.CompareTo(secend.PhoneNumber) == 1) return 1;
            else if (first.PhoneNumber.CompareTo(secend.PhoneNumber) == -1) return -1;
            else return 0;
        }
    }

    class BirthComparer : HostComparer, IComparer
    {
        public int Compare(object x, object y)
        {
            PhoneInfo first = x as PhoneInfo;
            PhoneInfo secend = y as PhoneInfo;

            if (first.Birth.CompareTo(secend.Birth) == 1) return 1;
            else if (first.Birth.CompareTo(secend.Birth) == -1) return -1;
            else return 0;
        }
    }

    class MajorComparer : HostComparer, IComparer
    {
        public int Compare(object x, object y)
        {
            if (!(x is PhoneUnivInfo) && !(y is PhoneUnivInfo)) return 0; //둘다 PhoneUnivInfo 가 아니면
            else if ((x is PhoneUnivInfo) && !(y is PhoneUnivInfo)) return -1; // 첫번째만 PhoneUnivInfo 이면
            else if (!(x is PhoneUnivInfo) && (y is PhoneUnivInfo)) return 1; // 두번째만 PhoneUnivInfo 이면

            PhoneUnivInfo first = x as PhoneUnivInfo;
            PhoneUnivInfo secend = y as PhoneUnivInfo;

            if (first.Major.CompareTo(secend.Major) == 1) return 1;
            else if (first.Major.CompareTo(secend.Major) == -1) return -1;
            else return 0;
        }
    }

    class YearComparer : HostComparer, IComparer
    {
        public int Compare(object x, object y)
        {
            if (!(x is PhoneUnivInfo) && !(y is PhoneUnivInfo)) return 0; //둘다 PhoneUnivInfo 가 아니면
            else if ((x is PhoneUnivInfo) && !(y is PhoneUnivInfo)) return -1; // 첫번째만 PhoneUnivInfo 이면
            else if (!(x is PhoneUnivInfo) && (y is PhoneUnivInfo)) return 1; // 두번째만 PhoneUnivInfo 이면

            PhoneUnivInfo first = x as PhoneUnivInfo;
            PhoneUnivInfo secend = y as PhoneUnivInfo;

            if (first.Year.CompareTo(secend.Year) == 1) return -1;
            else if (first.Year.CompareTo(secend.Year) == -1) return 1;
            else return 0;
        }
    }

    class CompanyComparer : HostComparer, IComparer
    {
        public int Compare(object x, object y)
        {
            if (!(x is PhoneCompanyInfo) && !(y is PhoneCompanyInfo)) return 0; //둘다 PhoneUnivInfo 가 아니면
            else if ((x is PhoneCompanyInfo) && !(y is PhoneCompanyInfo)) return -1; // 첫번째만 PhoneUnivInfo 이면
            else if (!(x is PhoneCompanyInfo) && (y is PhoneCompanyInfo)) return 1; // 두번째만 PhoneUnivInfo 이면

            PhoneCompanyInfo first = x as PhoneCompanyInfo;
            PhoneCompanyInfo secend = y as PhoneCompanyInfo;

            if (first.Company.CompareTo(secend.Company) == 1) return 1;
            else if (first.Company.CompareTo(secend.Company) == -1) return -1;
            else return 0;
        }
    }

    class Compaer
    {
    }
}
