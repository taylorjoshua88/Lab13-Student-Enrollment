# Student and Course Administration

**Author**: Joshua Taylor
**Version**: 1.0.0

## Overview

Student and Course Adminstration demonstrates the implementation of create,
read, update, and delete (CRUS) operations across multiple tables within a 
database without using the scaffolding features found in ASP.NET Core, MVC, 
Entity Framework, and Microsoft Visual Studio.

The application provides an interface for administrators to quickly view all
of their educational institutions students and courses. In addition, these
administrators can create, read, update, and delete these entries. Logic is
provided to ensure that courses are not dropped which still have students
enrolled in them.

## Getting Started

Student and Course Administration targets the .NET Core 2.0 platform, ASP.NET Core, Entity
Framework Core and the MVC Framework. The .NET Core 2.0 SDK can be downloaded 
from the following URL for Windows, Linux, and macOS:

https://www.microsoft.com/net/download/

Additionally, the Entity Framework tools will need to be installed via the
NuGet Package Manager Console in order to create a migration for the local,
development database (run from the solution root):

    Install-Package Microsoft.EntityFrameworkCore.Tools
	Add-Migration Initial
	Update-Database

The dotnet CLI utility would then be used to build and run the application:

    cd StudentEnrollment
    dotnet build
    dotnet run

The _dotnet run_ command will start an HTTP server on localhost using Kestrel
which can be accessed by the user's browser pointing to localhost on the port
provided by _dotnet run_'s console output.

Additionally, users can build and run Movie Library using Visual Studio
2017 or greater by opening the solution file at the root of this repository.
All dependencies are referenced via NuGet and should be brought in during
the restore process. If this does not occur, the following will download all
needed dependencies (other than the Entity Framework tools):

    dotnet restore

Unit testing is provided via the xUnit framework and is included as a NuGet
dependency of the StudentEnrollmentTest project within the solution.

## Example

#### Overview of Students ####
![Students Overview Screenshot](/assets/studentOverview.JPG)
#### Adding Students ####
![Adding Students Screenshot](/assets/studentAdd.JPG)
#### View Student Profile ####
![Student Profile Screenshot](/assets/studentDetails.JPG)
#### Removing Student ####
![Removing Student Screenshot](/assets/studentRemove.JPG)
#### Overview of Courses ####
![Courses Overview Screenshot](/assets/courseOverview.JPG)
#### Course Details ####
![Course Details Screenshot](/assets/courseDetails.JPG)
#### Editing Courses ####
![Editing Course Screenshot](/assets/courseEdit.JPG)
#### Deleting Courses ####
![Deleting Course Screenshot](/assets/courseDelete.JPG)

## Architecture

### Controllers

#### HomeController ####

_HomeController_ provides actions for displaying static pages to the
user in the form of an _About_ action, an _Index_ action and a _Contact_ 
action.

#### CoursesController ####

_CoursesController_ provides actions for performing CRUD operations
on courses within the backend database. The _Index_ action provides
an overview of all courses along with filtering capabilities. The 
_Details_ action provides information about a specific courses as well
as providing links to the students currently enrolled in that course.
The _Edit_, _Create_, and _Delete_ actions provide forms for entering in
data to be updated or created on the database via HTTP GET that are
then delivered to the server for validation and database manipulation
via POST after the user submits them. In the case of _Delete_, this is 
simply a confirmation prompt. A token is used to ensure that forged POST 
requests are not accepted.

#### StudentsController ####

_StudentsController_ provides actions for performing CRUD operations
on students within the backend database. The _Index_ action provides
an overview of all students along with filtering capabilities. The
_Details_ action provides a specific student's profiles along with a
link to the course that that student is currently enrolled in. The
_Edit_, _Create_, and _Remove_ actions provide forms for entering in
data to be updated or created on the database via HTTP GET that are
then deliverd to the server for validation and database manipulation
via POST after the user submits them. In the case of _Remove_, this is
simply a confirmation prompt. A token is used to ensure that forged
POST request are not accepted.

### Data Model

#### Student ####

Students are represented using the _Student_ class on the code side. This
class contains the following fields:

- ID (primary key, int)
- FirstName (string)
- LastName (string)
- EnrollmentDate (DateTime)
- CurrentCourse (foreign reference, Course)
- CurrentCourseId (foreign key, int)
- HighestCourseLevel (int)
- PassedInterview (bool)
- Placed (bool)

#### Course ####

Courses are represented using the _Course_ class on the code side. This
class contains the following fields:

- ID (primary key, int)
- Name (string)
- Iteration (string)
- Level (int)
- StartDate (DateTime)
- EndDate (DateTime)
- Technology (enumeration)
- Instructor (string)

### Frontend

All frontend code has been developed using Bootstrap and JQuery. The
Cerulean CSS style for Bootstrap was used for this project and can be 
acquired [here](https://bootswatch.com/cerulean/).

All views have been optimized to display properly on any size screen.
Please contact the author if this is found to not be the case.

## Change Log

* 4.9.2018 [Joshua Taylor](mailto:taylor.joshua88@gmail.com) - Initial
release. All included tests are passing.
