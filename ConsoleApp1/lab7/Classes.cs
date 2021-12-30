using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    // структура
    struct Secretary
    {
        private string name;
        private string surname;
        private int age;

        public Secretary(string Name, string Surname, int Age)
        {
            name = Name;
            surname = Surname;
            age = Age;
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Фамилия – {name}, имя – {surname}, возраст – {age}.");
        }
    }
    // перечисление
    enum DocumentColor
    {
        White,
        Black,
        Yellow = 6,
        Red, // 7
        Blue // 8
    }

    // класс-контейнер для хранения разных типов объектов
    public class Bookkeeping
    {
        private Document[] array;

        public Bookkeeping(Document[] arr)
        {
            array = new Document[arr.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = arr[i];
            }
        }
        public Document this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }
        public Document[] Get() => array;
        public Document[] Set(Organization[] newArray)
        {
            if (array.Length < newArray.Length)
            {
                Array.Resize(ref array, newArray.Length);
            }
            Array.Copy(newArray, array, newArray.Length);
            return array;
        }
        public void Add(Document obj)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = obj;
        }
        public void Remove()    // удаление с конца
        {
            if (array.Length == 0)
            {
                Console.WriteLine("Список не содержит элементов");
            }
            else
            {
                Array.Resize(ref array, array.Length - 1);
            }
        }
        public void Display()
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
    }

    public static class Controller
    {
        public static double SumPrice(Bookkeeping bookkeepingArr, string name)
        {
            double sum = 0;
            Document[] array = (Document[])bookkeepingArr.Get();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] is Invoice)
                {
                    if (array[i].NameOfOrganization == name)
                    {
                        sum += array[i].Price;
                    }
                }
            }
            return sum;
        }
        public static int CheckAmount(Bookkeeping bookkeepingArr)
        {
            int checkAmount = 0;
            Document[] array = (Document[])bookkeepingArr.Get();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] is Check)
                    checkAmount++;
            }
            return checkAmount;
        }
        public static void DocumentsForDate(Bookkeeping bookkeepingArr, int dateDay, int dateMonth, int dateYear)
        {
            int num = 0;
            Document[] array = (Document[])bookkeepingArr.Get();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].GetDate().Day == dateDay && array[i].GetDate().Month == dateMonth && array[i].GetDate().Year == dateYear)
                {
                    num++;
                    Console.WriteLine($"{num}. {array[i]}");
                }
            }
        }
    }
    abstract public class Organization
    {
        private protected string nameOfOrganization;
        public Organization() { }
        public Organization(string orgName)
        {
            nameOfOrganization = orgName;
        }
        public string NameOfOrganization { get => nameOfOrganization; set => nameOfOrganization = value; }
        public abstract void Burning();
        public override string ToString() => $"Тип объекта – {this.GetType()}, название организации – {this.nameOfOrganization}";
    }
    public class Date
    {
        int year;
        int month;
        int day;
        public int Year
        {
            get => year;
            set
            {
                if (value < 1)
                {
                    throw new DateException("Значение года не может быть: меньше или равно нулю.");
                }
                else
                    year = value;
            }
        }
        public int Month
        {
            get => month;
            set
            {
                if (value < 1 || value > 12)
                {
                    throw new DateException("Значение месяца не может быть: меньше или равно нулю; больше 12");
                }
                else
                    month = value;
            }
        }
        public int Day
        {
            get => day;
            set
            {
                if (value < 1 || value > 31)
                {
                    throw new DateException("Значение дня не может быть: меньше или равно нулю; больше 31");
                }
                else
                    day = value;
            }
        }
        public Date(int dateDay, int dateMonth, int dateYear)
        {
            Day = dateDay;
            Month = dateMonth;
            Year = dateYear;
        }
        public override string ToString() => $"Тип объекта – {this.GetType()}, дата создания: {day}.{month}.{year}";
    }
    public class Document : Organization
    {
        private protected bool stamp;
        private protected Date date;
        private protected double price;
        public bool IsStamped { get => stamp; set => stamp = value; }
        public double Price
        {
            get => price;
            set => price = value;
        }
        public Date GetDate() => date;
        public Document() { }
        public Document(string organizationName, int dateDay, int dateMonth, int dateYear, bool isStamped, double docPrice)
            : base(organizationName)
        {
            date = new Date(dateDay, dateMonth, dateYear);
            stamp = isStamped;
            if (docPrice < 0)
                throw new PriceIsLessThanZeroException("Цена товара не может быть меньше нуля");
            price = docPrice;
        }


        // виртуальные методы, подлежащие переопределению

        virtual public void Store()
        {
            Console.WriteLine("Документ хранится в сейфе");
        }

        // реализация методов интерфейса

        public void Change()
        {
            Console.WriteLine("Документ изменен");
        }
        public void Find()
        {
            Console.WriteLine("Документ найден");
        }
        public void Lose()
        {
            Console.WriteLine("Документ утерян");
        }
        public override void Burning()
        {
            Console.WriteLine("Oh no! The document is burning!");
        }

        // Переопределенные методы Object

        /*public override string ToString() => $"Тип объекта – {this.GetType()}, организация – {this.NameOfOrganization}, " +
            $"дата создания – {date.Day}.{date.Month}.{date.Year}, есть ли печать – " + (stamp ? "есть" : "нет");*/
       // public override int GetHashCode() => HashCode.Combine(this.NameOfOrganization, date.Day, date.Month, date.Year);
        public override bool Equals(object obj)
        {
            if (obj is Document objectType)
            {
                if (this.date.Day == objectType.date.Day
                        && this.date.Month == objectType.date.Month
                            && this.date.Year == objectType.date.Year
                                && this.NameOfOrganization == objectType.NameOfOrganization)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class Receipt : Document //квитанция
    {
        private protected string docType;
        public string DocType { get => docType; set => docType = value; }
        public Receipt(string organizationName, int dateDay, int dateMonth, int dateYear, bool isStamped, string documentType, double docPrice)
            : base(organizationName, dateDay, dateMonth, dateYear, isStamped, docPrice)
        {
            docType = documentType;
        }
        public Receipt() { }
        public override void Store()
        {
            Console.WriteLine("Этот документ хранится в ящике");
        }
        public override string ToString() => base.ToString() + $", тип документа – {this.DocType}";
    }
    public class Invoice : Document //накладная
    {
        private protected string docType;
        public string DocType { get => docType; set => docType = value; }
        public Invoice(string organizationName, int dateDay, int dateMonth, int dateYear, bool isStamped, string documentType, double docPrice)
            : base(organizationName, dateDay, dateMonth, dateYear, isStamped, docPrice)
        {
            foreach (char ch in documentType)
            {
                if (ch == '1' || ch == '2' || ch == '3' || ch == '4' || ch == '5' || ch == '6' || ch == '7' || ch == '8' || ch == '9')
                {
                    throw new InvalidDocTypeException("Название типа документа не может содержать цифр!");
                }
            }
            docType = documentType;
        }
        public Invoice() { }
        public override void Store()
        {
            Console.WriteLine("Этот документ хранится на полке");
        }
        public override string ToString() => base.ToString() + $", тип документа – {this.DocType}";
    }
    sealed public partial class Check : Document   //чек
    {
        private string docType;
        public string DocType { get => docType; set => docType = value; }
        public Check(string organizationName, int dateDay, int dateMonth, int dateYear, bool isStamped, string documentType, double docPrice)
            : base(organizationName, dateDay, dateMonth, dateYear, isStamped, docPrice)
        {
            docType = documentType;
        }
        public Check() { }
        public override void Store()
        {
            Console.WriteLine("Этот документ хранится в стопке");
        }
        public override string ToString() => base.ToString() + $", тип документа – {this.DocType}";
    }
    public class Printer
    {
        public void IAmPrinting(Organization someobj)
        {
            Console.WriteLine(someobj.ToString());
        }
    }
}
