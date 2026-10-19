using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Student
    {
        private string _fullName;
        private int _birthYear;
        private int _school;
        private string _id;

        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ПІБ не може бути порожнім.", nameof(FullName));
                }
                _fullName = value;
            }
        }

        public int BirthYear
        {
            get { return _birthYear; }
            set
            {
                if (value > DateTime.Now.Year)
                {
                    throw new ArgumentException("Рік народження не може бути в майбутньому.", nameof(BirthYear));
                }
                if (value < 1926)
                {
                    throw new ArgumentException("Рік народження не може бути менше за 1900.", nameof(BirthYear));
                }
                _birthYear = value;
            }
        }

        public int School
        {
            get { return _school; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Номер школи повинен бути більше за 0.", nameof(School));
                }
                _school = value;
            }
        }

        public string Id
        {
            get { return _id; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ID не може бути порожнім.", nameof(Id));
                }
                _id = value;
            }
        }

        public Student(string fullName, int birthYear, int school, string id)
        {
            FullName = fullName;
            BirthYear = birthYear;
            School = school;
            Id = id;
        }


        public int CompareTo(Student other)
        {
            if (other == null) return 1;
            return string.Compare(this.Id, other.Id, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"ПІБ: {FullName}, Рік народження: {BirthYear}, Школа: {School}, ID: {Id}";
        }
    }

}