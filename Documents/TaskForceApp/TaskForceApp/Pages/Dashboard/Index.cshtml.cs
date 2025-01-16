using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace TaskForceApp.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
           
        }

        [HttpGet]
        public async Task<JsonResult> OnGetTotals()
        {
            decimal totalIncome = 0;
            decimal totalExpense = 0;

            try
            {
                string connectionString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=TaskForceDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sqlQuery = "SELECT SUM(income) AS totalIncome FROM ExpenseTracker;";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        object result = await cmd.ExecuteScalarAsync();
                        totalIncome = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }

                    string sqlQuery1 = "SELECT SUM(expense) AS totalExpense FROM ExpenseTracker;";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery1, con))
                    {
                        object result = await cmd.ExecuteScalarAsync();
                        totalExpense = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
              
            }

            return new JsonResult(new { totalIncome, totalExpense });
        }
    }
}
