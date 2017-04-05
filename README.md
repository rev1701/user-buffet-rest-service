# User Buffet web service
REST web service to manage users and candidate scores, using ASP.NET Web API REST web service


---
# technologies
- ASP.NET
- Web API
- JSON
- C#


---
# team
(USL) User Scores Login
- Alain Bruno
- Erik May
- Michael Furlow
- Stephen Kirkland


---
# license
Apache 2.0 (https://github.com/rev1701/user-buffet-rest-service/blob/master/LICENSE)


---
# Documentation

EC2 Instance: http://ec2-54-215-138-178.us-west-1.compute.amazonaws.com/UserBuffetService/api/

## UsersController
  
  ### -users/
    Gets all users
  
  ### -users/GetUser?id=(int)||GetUser?email= (string)
    Returns a user. Can pass in int or user eamil
    
  ### -users/CheckUser?email=(string)&password= (string)
    checks for user and returns a user
   
  ### -users/EditUser?email=&userObj (string, User)
    Takes in email string to identify user entity to edit and modified user object to save the changed data from
    
  ### -users/CreateUser (User)
    Takes user object and saves it to database.
    
## BatchesController

  ### -batches/
    Gets all batches

  ### -batches/GetBatch?batchID= (string)
    Returns a batch with its roster and exams. Pass in batchId string
    
  ### -batches/GetBatches?email=(string)
    Returns all the batches for a user

  ### -batches/EditBatch?id=&batchObj (string, Batch)
    Takes in batchID string to identify batch entity to edit and modified batch object to save the changed data from

  ### -batches/CreateBatch(Batch)
    Takes Batch object and saves it to database.

## ExamAssessmentController

  ### -examassessments/
    Gets all exam assessments

  ### -examassessments/GetUserExams?email= (string)
    Returns all exams for the a user based on string email passed in.
    
  ### -examassessments/GetExamAssessment?email=&examSettingsID= (string, int)
    Returns all exams for the a user based on string email passed in.

  ### -examassessment/EditExam?email=&settingsId=&examAssessmentObj (string, int, ExamAssessment)
    Takes in email string to and examsettingsID int  to find a users exam assessment to edit and uses examassessment object passed in to as the data to be stored
    
  ### -examassessment/SaveExam (ExamAssessment)
    stores examassessment into the database
    
## ExamSettingsController

  ### -examsettings/
    Gets all exam settings
    
  ### -examsettings/GetSetting?id= (int)
    Gets the setting by ID

  ### -examsettings/ModifySettings?id=&examSettingObj (id, examsetting)
    Changes an exam setting with the exam setting object passed in

  ### -examsettings/StoreSettings (examSetting)
    Takes an exam setting and stores it
   
  ### -examsettings/AssignExamToUser?email=&examSettingID= (string, int)
    Assigns a setting to a user
    
  ### -examsettings/AssignExamToBatch?batchId=&examSettingID= (string, int)
    Assigns a setting to a batch
    
  ### -examsettings/AddQuestion (ExamQuestion)
    stores an exam question for a setting to the database
</>
