using System;
using System.Collections.Generic;
using System.Data.SqlClient; //SqlException
using MySql.Data.MySqlClient;

namespace Connect
{
	public class EmployeesTable
	{
		public int id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string email { get; set; }
	}

	class Connect
	{
		static List<EmployeesTable> employees = new List<EmployeesTable>();
		static void Main(string[] args)
		{
			//Csatlakozas();
			Kivetelkezeles();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		static void Csatlakozas()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;


			string connString = "server=localhost;database=classicmodels;uid=teszt;pwd=abc123";
			MySqlConnection conn = new MySqlConnection(connString);
			Console.WriteLine("Kapcsolat megnyitása...");
			conn.Open();
			Console.WriteLine("Kapcsolat megnyitva");

			string query = "SELECT * FROM employees";
			MySqlCommand command = new MySqlCommand(query, conn);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				EmployeesTable employee = new EmployeesTable
				{
					id = Convert.ToInt32(reader["employeeNumber"]),
					firstName = reader["firstName"].ToString(),
					lastName = reader["lastName"].ToString(),
					email = reader["email"].ToString()
				};
				employees.Add(employee);
			}

			reader.Close();
			conn.Close();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("SQL-ből kiolvasott adatok:");

			Console.ForegroundColor = ConsoleColor.DarkGreen;
			foreach (var item in employees)
			{
				Console.WriteLine($"\t{item.id} | {item.firstName} | {item.lastName} | {item.email}");
			}
		}

		static void Kivetelkezeles()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			string connString = "server=localhost;database=classicmodels;uid=teszt;pwd=abc123";
			MySqlConnection conn = new MySqlConnection(connString);
			Console.WriteLine("Kapcsolat megnyitása...");

			try
			{
				conn.Open();
				Console.WriteLine("Kapcsolat megnyitva");
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Kapcsolódási hiba: " + e.Message);
			}

			string query = "SELECT * FROM employees";
			MySqlCommand command = new MySqlCommand(query, conn);

			try
			{
				MySqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					EmployeesTable employee = new EmployeesTable
					{
						id = Convert.ToInt32(reader["employeeNumber"]),
						firstName = reader["firstName"].ToString(),
						lastName = reader["lastName"].ToString(),
						email = reader["email"].ToString()
					};
					employees.Add(employee);
				}
				reader.Close();
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("SQL hiba: " + e.Message);
			}

			conn.Close();

			if (employees.Count != 0)
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("SQL-ből kiolvasott adatok:");
				Console.ForegroundColor = ConsoleColor.DarkGreen;

				foreach (var item in employees)
				{
					Console.WriteLine($"\t{item.id} | {item.firstName} | {item.lastName} | {item.email}");
				}
			}

		}
	}
}
