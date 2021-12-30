using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Document receipt = new Receipt("ООО \"Севастополь\"", 13, 10, 2020, true, "квитанция", 11);
            Document invoice1 = new Invoice("ООО \"Севастополь\"", 12, 12, 2020, true, "накладная", 2.4);
            Document check = new Check("ООО \"Братислава\"", 12, 10, 2020, true, "чек", 9.2);
            Document check1 = new Check("ООО \"Севастополь\"", 10, 10, 2020, true, "чек", 3.5);
            Document receipt1 = new Receipt("ООО \"Севастополь\"", 19, 11, 2020, true, "квитанция", 1.12);

            Document[] someArray = { receipt, check1, check, invoice1, receipt1 };
            Bookkeeping arrayOfDocuments1 = new Bookkeeping(someArray);
            Bookkeeping arrayOfDocuments2 = new Bookkeeping(someArray);

            try // 1 - InvalidDocTypeException
            {
                Document invoice = new Invoice("ООО \"Севастополь\"", 12, 10, 2020, true, "накладная12", 10.1);
            }
            catch (InvalidDocTypeException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}, объект, вызвавший исключение: {ex.Source}, метод, в котором вызвано исключение: {ex.TargetSite}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}, объект, вызвавший исключение: {ex.Source}, метод, в котором вызвано исключение: {ex.TargetSite}");
            }

            Console.WriteLine("=====================\n\n");
            try // 2 - PriceIsLessThanZeroException
            {
                Document document = new Document("ООО \"Братислава\"", 12, 10, 2020, true, -1.1);
            }
            catch (PriceIsLessThanZeroException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}, объект, вызвавший исключение: {ex.Source}, метод, в котором вызвано исключение: {ex.TargetSite}");
            }

            Console.WriteLine("=====================\n\n");
            try // 3 - индекс за пределами массива
            {
                Document s = someArray[5];
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}, объект, вызвавший исключение: {ex.Source}, метод, в котором вызвано исключение: {ex.TargetSite}");
            }


            Console.WriteLine("==================\n\n");
            try // 4 - ссылка на null
            {
                Document doc = null;
                if (doc == null)
                    throw new NullReferenceException("Нельзя ссылаться на объект, значение котрого null");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}, объект, вызвавший исключение: {ex.Source}, метод, в котором вызвано исключение: {ex.TargetSite}");
            }

            Console.WriteLine("=======================\n\n");
            try // 5 - DateException
            {
                Document document = new Document("ООО \"Братислава\"", -12, 10, 2020, true, -1.1);
            }
            catch (DateException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}, объект, вызвавший исключение: {ex.Source}, метод, в котором вызвано исключение: {ex.TargetSite}");
            }
            catch // универсальный обработчик
            {
                Console.WriteLine("Произошла непредвиденная ошибка");
            }
            finally
            {
                Console.WriteLine("Блок finally на случай появления необрабатываемых исключений");
            }

            Console.WriteLine("====================");
            Debug.Assert(true); // проверяется условие, если оно false, то отображается окно сообщения со стеком вызовов

            arrayOfDocuments2.Add(receipt1);
            Console.WriteLine("Вывод второго массива после добавления элемента");
            arrayOfDocuments2.Display();

            Console.WriteLine("\n\n");


            arrayOfDocuments1.Remove();
            Console.WriteLine("Вывод первого массива после удаления элемента");
            arrayOfDocuments1.Display();
            Console.WriteLine("\n\n\n");

            Console.WriteLine(":Суммарная стоимость продукции заданного наименования по всем накладным:");
            Console.WriteLine("Введите наименование");
            string name = Console.ReadLine();
            Console.WriteLine($"Сумма равна: {Controller.SumPrice(arrayOfDocuments1, name)}");
            Console.WriteLine("");

            Console.WriteLine($"Количество чеков в массиве равно: {Controller.CheckAmount(arrayOfDocuments1)}");
            Console.WriteLine("");

            Console.WriteLine(":Вывод документов за указанную дату:");
            Console.WriteLine("Введите день: ");
            int day = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите месяц: ");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите год: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Вывод документов");
            Controller.DocumentsForDate(arrayOfDocuments1, day, month, year);
            Console.ReadKey();
        }
    }
}
