using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public abstract class TouristTour
    {
        private string country = "";
        private int days;
        private double price;
        private int peopleCount;
        private bool isBooked;

        public string Country
        {
            get { return country; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Країна не може бути порожньою.", nameof(value));
                }
                country = value;
            }
        }

        public int Days
        {
            get { return days; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Кількість днів повинна бути більшою за 0.", nameof(value));
                }
                days = value;
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Ціна за 1 особу повинна бути більшою за 0.", nameof(value));
                }
                price = value;
            }
        }

        public int PeopleCount
        {
            get { return peopleCount; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Кількість осіб повинна бути більшою за 0.", nameof(value));
                }
                peopleCount = value;
            }
        }

        public bool IsBooked
        {
            get { return isBooked; }
            protected set { isBooked = value; }
        }

        public TouristTour(string country, int days, double price, int peopleCount)
        {
            Country = country;
            Days = days;
            Price = price;
            PeopleCount = peopleCount;
            IsBooked = false;
        }

        public virtual double CalculateTotalPrice()
        {
            return Price * PeopleCount;
        }

        public virtual string Book()
        {
            if (IsBooked)
            {
                throw new InvalidOperationException("Тур уже заброньовано.");
            }

            IsBooked = true;
            return $"Тур до країни {Country} на {Days} днів на {PeopleCount} осіб заброньовано. Загальна вартість: {CalculateTotalPrice():C}.";
        }

        public virtual string Cancel()
        {
            if (!IsBooked)
            {
                throw new InvalidOperationException("Тур ще не було заброньовано.");
            }

            IsBooked = false;
            return $"Бронювання туру до країни {Country} скасовано.";
        }
    }
}