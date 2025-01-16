using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace TaskForceApp.Pages.ExpenseTracker
{
    public class AddIncomeModel : PageModel
    {
        [BindProperty]
        public string description { get; set; }

        [BindProperty]
        public string income { get; set; }

        [BindProperty]
        public string account { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(income) || string.IsNullOrEmpty(account))
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }

            try
            {
                //string hashedPassword = HashPassword(Password);

                string connectionString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=TaskForceDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "INSERT INTO ExpenseTracker (income,expense,remaining,account,description) VALUES (@income, 0, 0, @account, @description)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@income", income);
                        cmd.Parameters.AddWithValue("@account", account);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.ExecuteNonQuery();
                    }
                }
                SuccessMessage = "Income recorded";
                TempData["SuccessMessage"] = SuccessMessage;
                return RedirectToPage("/ExpenseTracker/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
