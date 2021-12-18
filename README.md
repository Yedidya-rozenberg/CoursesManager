# CoursesManager
Online school demo

Yedidya Rozenberg
Online school Demo
(C#  Project)
# 1.	About This Project
This project is designed to demonstrate an online learning system. It was built to demonstrate knowledge and implementation of C# and Entity Framework Programming fundamentals and principles.
# 2.	Built With :
The project was Built with Entity Framework Core as the data access technology, and SQL Server as the database structure. The user interface was built on the console, while ensuring separation between the layers, in order to allow easy transition to the HTML interface later on (which did not happen due to time constraints).
Programming	Data Access	Database	GUI
C#	Entity Framework Core.	SQL Server	Console
The project loosely implements the Singleton design pattern - All entities in the DAL layer access the database through the same instance, in order to avoid an access conflict. At the same time, the solution was designed through an additional class and not through a ScoolContext class, to allow EF to build the database using the code-first method.
In addition, the project actually states the design pattern state in the course access department - with some options fixed in the CoursContext department, and some varying according to the user's definition - whether he is a teacher or a student, whether he is associated with the course, and whether the course is active, canceled or edited.
# 3.	Getting Started:
•	_ Download the code source: _
Download or clone the Github code from: https://github.com/Yedidya-rozenberg/CoursesManager
_ Running the Demo: _
In the "Package manager Console" enter "update-database" in order to build the database, if not exist. After this, open "program" file and run it to full demo data to database. Comment the 19 line ( FullDatabase();) , recommend 20 line ( Display.LogginScreen();), and run again.
You can choose to connect as a teacher or as a student. The list of usernames and passwords appears in program class, in the FullDatabase static method.
# 4.	Entities, Relations design and logic:
The project implements EF TPT (Table-per-Type) inheritance model. Table-per-type inheritance uses a separate table in the database to maintain data for non-inherited properties and key properties for each type in the inheritance hierarchy.
Users
The first table of users includes only a username, password and one-to-one link to the table with the rest of the details - so that additional security can be created for this table.

  public class UserLoggin
  
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string  UserName { get; set; }
        [Required]
        public string Password { get; set; }

        [ForeignKey("Student")]
        public int? StudentID { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Teacher")]
        public int? TeacherID { get; set; }
        public Teacher Teacher { get; set; }
    }
    
There are two types of users - teacher and user. I was debating whether to produce one class that both would inherit from, and in the end I decided to create them as two different classes, in order to make the demo more reliable.

  public class Student //:UserDetiles
  
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LestName { get; set; }

        [Phone]
        public int PhonNumber { get; set; }
        [EmailAddress]
        public string email { get; set; }

        public UserLoggin userLoggin { get; set; }

        public float Payment { get; set; }

        public ICollection<Cours> Cours { get; set; }
    }
    
  public class Teacher //: UserDetiles
  
    {
        [Key]
        public int TeacherID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LestName { get; set; }

        [Phone]
        public int PhonNumber { get; set; }
        [EmailAddress]
        public string email { get; set; }

        public UserLoggin userLoggin { get; set; }

        public float Selery { get; set; }

        public ICollection<Cours> TeachCours{ get; set; }
        }
        
 It was later planned to add a third user, of the administrator type - who would approve or reject requests, and could add or remove other users.
The main object of the project is a course - which includes a link to the teacher, students, and teaching units that it includes - which include the study material and questions about the material.

  public class Cours
  
    {
        [Key]
        public int CoursID { get; set; }
        
        [Required]
        public string CoursName { get; set; }
        
        [Required]
        public char CuorsStatus { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Unit> Units { get; set; }
    }
    
public class Unit

    {    
        [Key]
        public int UnitID { get; set; }
        [ForeignKey("Cours")]
        public int coursID { get; set; }
        public Cours Cours { get; set; }

        [Required]
        public string UnitName { get; set; }

        public string StudyContent { get; set; }
        public string Questions { get; set; }
    }
    
In addition to that, I created a class request that is designed to document and approve critical actions like activating and canceling a course, enrolling and canceling a course registration.

   public class Request
   
    {
    
        public Request()
        {
            this.RequestTime = DateTime.Now;
            this.RequestStatus = "Open";
        }
        public Request(int coursID, char requesrCode, string requestDetiles, int? studentID = null, int? teacherID = null)
        {
            this.CoursID = coursID;
            this.RequestCode = requesrCode;
            this.RequestDetiles = requestDetiles;
            this.StudentID = studentID;
            this.TeacherID = teacherID;
            this.RequestTime = DateTime.Now;
            this.RequestStatus = "Open";
        }
        [Key]
        public int RequestID { get; set; }
        [ForeignKey("Cours")]
        public int CoursID { get; set; }
        public Cours Cours { get; set; }

        [ForeignKey("Student")]
        public int? StudentID { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Teacher")]
        public int? TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public char RequestCode { get; set; }
        [Required]
        public string RequestDetiles { get; set; }
        [Required]
        public DateTime RequestTime { get; set; }
        [Required]
        public string RequestStatus { get; set; }
    }
    
# 5.	Usage and Logic:
The DAL assembly offer a various data retrieving / manipulation methods to be called via static classes. Following is a key list of these methods:

 **Teacher methods:**
•	Adding course*
•	View and edit privet details.
•	Edit course – course name, add/edit/remove units.
•	Activate/cancellate courses.
•	View course list – my courses, other courses or all courses.
 *not implemented in the GUI due to lack of time.*
**Student methods:**
•	View course list – my courses, other courses or all courses.
•	Enter to course and view details – teacher connect details and unit's names.
•	View the study material and questions in the course units to which he is enrolled.
•	Registration and cancellation of course registration
**Login :**
Upon logging in, the user is prompted to enter a username and password. During data validation the system retrieves and retains its personal details in the program variable.

