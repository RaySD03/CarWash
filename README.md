- Project requirements

Add service account credentials json file:<br /><br />
    Since user authentication is handled by Google Firebasebase, the app relies on a service account credentials json file.
    Relevent information about generating the json file can be found here [Google Firebase](https://firebase.google.com/docs/cloud-messaging/auth-server#:~:text=To%20authenticate%20a%20service%20account,confirm%20by%20clicking%20Generate%20Key).
    The generated json file must be placed in the "Resources/Raw/" folder.
    
Note:<br /><br />
In addition, the project also relies on Google Firestore to access and store user/agent data. As a result, the db and agent selection related views should be configured for the list of agents. 
This aspect is still in progress and will need to be updated to access agents from the db based on user's entered zipcode in the profile tab.

- Project details

Home screen:

<img src="https://github.com/RaySD03/CarWash/assets/113494325/e899f891-9987-4032-bd1b-efa4123ae026" width="500" height="515">


<br /><br />
List of scheduled appointments:

<img src="https://github.com/RaySD03/CarWash/assets/113494325/cdb10699-4f06-4d98-89e3-59d7ae4ac851" width="500" height="505">


<br /><br />
User profile:

<img src="https://github.com/RaySD03/CarWash/assets/113494325/29012483-7d74-43e3-a0e6-f5f295466a6f" width="500" height="515">


<br /><br />
Appointment details:

<img src="https://github.com/RaySD03/CarWash/assets/113494325/a9b94601-6be2-4169-8e7d-1da1a6f43604" width="500" height="515">


<br /><br />
Agent selection:

<img src="https://github.com/RaySD03/CarWash/assets/113494325/ba488ddd-0253-4967-b49a-1bd586c921f7" width="500" height="515">
