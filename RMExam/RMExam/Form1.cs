/* Revision History: 10/20/2023, Rutvi Mistry: Created */
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMExam
{
    public partial class Form1 : Form
    {
        //Hashtable to store new Employee which have to add
        Hashtable AddEmployee = new Hashtable();

        //Queue to store delete employee
        Queue DeleteEmployee = new Queue();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        //Event Handler for Delete Employee Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (DeleteEmployee.Count > 0)
            {
                // Remove the first added employee from the queue
                Employee deleteEmployee = (Employee)DeleteEmployee.Dequeue();

                // Remove employee from hashtable 
                AddEmployee.Remove(deleteEmployee.Name);

                // Show success message when employee deleted
                MessageBox.Show("Employee Deleted Successfully", "Rutvi Mistry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine($"Employee {deleteEmployee.Name} with Salary {deleteEmployee.Salary} removed successfully", "Rutvi Mistry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Show error message if the queue is empty
                MessageBox.Show("Employee Can not found in list", "Rutvi Mistry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Event handler for AddEmployee Button
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            //Data retrive from text box for name and address and stored in variable 
            string name = txtName.Text;
            string address = txtAddress.Text;

            // VAlidation for name, address and salary

            //Check for empty field of name and if it is empty than show an error message in messageBox
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Error in Name: Name Can not be empty", "Rutvi Mistry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check for empty field of address and if it is empty than show an error message in messageBox
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Error in Address: Address Can not be empty", "Rutvi Mistry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check for salary format if it is not in format than show an error message in messagebox
            if (!double.TryParse(txtSalary.Text , out double salary))
            {
                MessageBox.Show("Error in Salary: Input String Was not in correct format", "Rutvi Mistry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check is salary in less than 0, if it is than shoe an error message in messagebox
            if(salary < 0)
            {
                MessageBox.Show("Error in Salary: Salary must be Positivie", "Rutvi Mistry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create new object for employee
            Employee employee = new Employee()
            {
                Name = name,
                Address = address,
                Salary = salary
            };

            //Add the employee to Hashtable and Queue
            AddEmployee.Add(name, employee);
            DeleteEmployee.Enqueue(employee);

            //Show message box with successfull message when employee added successFully
            MessageBox.Show("Employee Added Successfully", "Rutvi Mistry", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields after adding an employee
    txtName.Text = "";
    txtAddress.Text = "";
    txtSalary.Text = "";
        }

        //Event handler for PrintAll Button
        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            //Check if there is no record exist then show an error message in messagebox
            if(AddEmployee.Count == 0)
            {
                MessageBox.Show("No Record Found", "Rutvi Mistry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //To print all details about employee in Console

            int index = 1;
            foreach (DictionaryEntry entry in AddEmployee)
            {
                Employee employee = (Employee)entry.Value;

                Console.WriteLine($"{index}----------");
                Console.WriteLine($"Name: {employee.Name}");
                Console.WriteLine($"Address: {employee.Address}");
                Console.WriteLine($"Salary: {employee.Salary}");
                Console.WriteLine("");
                index++;
            }


        }
    }
}
