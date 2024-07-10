namespace HomeWorkEvents
{
	public class Program
	{
		public delegate int Operation(int x, int y);

		public delegate void Method();

		public delegate Message MessageBuilder(string text);

		public delegate void EmailReceiver(EmailMessage message);
		static void Main(string[] args)
		{
			Method method = delegate ()//вызов анонимного метода без параметров
			{
				Console.WriteLine("Hello, world");
			};
			method();

			Method methodLambda1 = () => Console.WriteLine("Hello, world");//вызов анонимного метода без параметров через лямбду
			methodLambda1();


			int z = 8;
			Operation operation = delegate (int x, int y)//вызов анонимного метода с параметрами
			{
				return x + y + z;
			};

			int result = operation(4, 5);
			Console.WriteLine(result);

			Operation methodLambda2 = (x, y) => x + y + z;//вызов анонимного метода с параметрами через лямбду
			result = methodLambda2(4, 5);
			Console.WriteLine(result);


			Console.WriteLine("--------------------events--------------------------");

			Account account = new Account(100);
			account.Notify += DisplayMessage;   // Добавляем обработчик для события Notify 
			/*account.Notify += message => Console.WriteLine(message);*/ //Установка в качестве обработчика лямбда-выражения
			account.Put(20);    // добавляем на счет 20
			Console.WriteLine($"Сумма на счете: {account.Sum}");
			account.Take(70);   // пытаемся снять со счета 70
			Console.WriteLine($"Сумма на счете: {account.Sum}");
			account.Take(180);  // пытаемся снять со счета 180
			Console.WriteLine($"Сумма на счете: {account.Sum}");

			void DisplayMessage(string message) => Console.WriteLine(message);

			Console.WriteLine("---------------ковариантность---контравариантность---------------------------");


			// делегату с базовым типом передаем метод с производным типом
			MessageBuilder messageBuilder = WriteEmailMessage; // ковариантность
			Message message = messageBuilder("Hello");
			message.Print();    // Email: Hello

			EmailMessage WriteEmailMessage(string text) => new EmailMessage(text);


			// делегату с производным типом передаем метод с базовым типом
			EmailReceiver emailBox = ReceiveMessage; // контравариантность
			emailBox(new EmailMessage("Welcome"));  // Email: Welcome

			void ReceiveMessage(Message message) => message.Print();

			Console.WriteLine("---------------------Action---------------------");
			// Объявление делегата Action с одним int параметром
			Action<int> printNumber = (number) => Console.WriteLine($"Число: {number}");

			// Вызов делегата
			printNumber(10); // Выведет: "Число: 10"

			Console.WriteLine("---------------------Predicate---------------------");
			// Объявление делегата Predicate для проверки, четное ли число
			Predicate<int> isEven = (number) => number % 2 == 0;

			// Вызов делегата
			bool result1 = isEven(4); // result будет true
			Console.WriteLine($"Четное ли число: {result1}"); // Выведет: "Четное ли число: True"

			Console.WriteLine("---------------------Func---------------------");
			// Объявление делегата Func для сложения двух чисел
			Func<int, int, int> sum = (a, b) => a + b;

			// Вызов делегата
			int result2 = sum(5, 7); // result будет 12
			Console.WriteLine($"Сумма: {result2}"); // Выведет: "Сумма: 12"


			Console.WriteLine("--------------Замыкания-------------------------------");

			var counter = new Counter();

			counter.Increment();
			counter.Increment();

		}
	}
}