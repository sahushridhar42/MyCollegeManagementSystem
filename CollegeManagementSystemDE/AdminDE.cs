using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollegeManagementSystemDE
{
    public class AdminDE
    {
    }

    public class Branch
    {
        public int BranchID { get; set;}
        public string BranchName { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }  
        public double Fees { get; set; }
        public string Criteria { get; set; }

    }

    public class NoticeDE
    {
        public int NoticeID { get; set; }
        public int BranchID { get; set; }
        public string AcadamicYear { get; set; }
        public string Date { get; set; }
        public string Notice { get; set; }
        public string Role { get; set; }
        public string BranchName { get; set; }
    }

    public class UserDE
    {
        public string  UserID{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string FullName { get; set; }
        public string AdmissionYear{get;set;}
        public string CurrentAddress{get;set;}
        public string PermanentAddress{get;set;}
        public string ContactNo{get;set;}
        public string EmailID{get;set;}
        public string Gender{get;set;}
        public string LoginName{get;set;}
        public string Password{get;set;}
        public string Role { get; set; }
        public string AcadamicYear { get; set; }
        public int BranchID{get;set;}
        public string Branch { get; set; }
        public int GardianID{get;set;}
        public string GardianName{get;set;}
        public string Contact{get;set;}
        public string Address{get;set;}
        public string Profession{get;set;}
  
    }

    public class ExaminationDE
    {
       public int ExaminationID{get;set;}
       public string  Examination{get;set;}
       public string  AcadamicYear{get;set;}
       public int BranchID { get; set; }

    }
    public class timetable
    {
         public int ExaminationID{get;set;}
         public string  Examination{get;set;}
         public string  AcadamicYear{get;set;}
         public int BranchID { get; set; }
         public string BachName { get; set; }
         public int ExaminationDetailID { get; set;}
         public string Subject{get;set;}
         public string Time{get;set;}
         public string Date{get;set;}
         public string Marks { get; set; }

    }

    public class Payroll
    {
         public int PaymentID{get;set;}
         public string  FullName {get;set;}
         public string  AcadamicYear{get;set;}
        public string Branch{get;set;}
         public string  Month{get;set;}
         public string  Amount{get;set;}
        public decimal DA{get;set;}
         public decimal  HRA{get;set;}
         public decimal  GrossSalary{get;set;}
         public decimal  PF{get;set;}
         public decimal NetAmount { get; set; }

    }

    public class Payroll1
    {
        public int PaymentID { get; set; }
        public string FullName { get; set; }
        public string AcadamicYear { get; set; }
        public string Branch { get; set; }
        public string Month { get; set; }
        public decimal Amount { get; set; }
        public decimal DearnessAllowance { get; set; }
        public decimal MedicalAllowance { get; set; }
        public decimal HouseRentAllowance { get; set; }
        public decimal ConveyanceAllowance { get; set; }       
        public decimal Overtime { get; set; }
         public decimal EmployeeStateInsurance { get; set; }
        public decimal ProvidentFund { get; set; }
        public decimal ProfessionalTax { get; set; }      
        public decimal NetAmount { get; set; }
         public decimal TotalDeduction { get; set; }
         public decimal TotalEarn { get; set; }
    }

    public class ResultDE
    {
        public int  ResultID{get;set;}
        public int BranchID{get;set;}
        public string UserID{get;set;}
        public int ExaminationID{get;set;}
        public string Examination { get; set; }
        public string Subject{get;set;}
        public string Marks { get; set;}

    }
    //yuvaraj
    public class FeesMastes
    {
        public Double Amount { get; set; }
        public int FeeMasterID { get; set; }
        public double RemainingAmount { get; set; }
    }

    public class FeeTransaction
    {
        public int FeeTransactionID { get; set; }
        public int UserID { get; set; }
        public int FeeMasterID { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Status { get; set; }
        public decimal Amount { get; set; }
        public decimal RemAmt { get; set; }
    }

    public class UserforAttendanceDE
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int BranchID { get; set; }
        public string Branch { get; set; }
        public string AcadamicYear { get; set; }

    }

    public class classtimetable
    {
        public string Day { get; set; }
        public string Subject { get; set; }
        public string Time { get; set; }
    }
    }
