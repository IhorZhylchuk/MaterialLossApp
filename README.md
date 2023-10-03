## MaterialLossApp
This project is a sampe WEB application for managing production orders (CRUD), accounting the waste of every ingredient which were used for the order and generating PDF reports, which include information about realized order, QA parameters and 
wasted ingredients.

### Prerequisites
To run this project, you'll need the following:

.NET 7  SDK installed on your machine, an IDE such as Visual Studio or Visual Studio Code installed and SQL Server.

Clone this repository to your local machine:<br/>
<b>````git clone https://github.com/IhorZhylchuk/MaterialLossApp.git````</b>
<br/>
<br/>
Navigate to the project directory and restore dependencies:
<br/>
<b>```cd YOUR_REPOSITORY```</b><br/>
<b>```dotnet restore```</b><br/>

Open the project in IDE, in <b>```appsettings.json```</b> enter your server name, run the following commands:<br/>
<b>```add-migration MIGRATION_NAME```</b> then run <br/>
<b>```update-database```</b><br/>
[This video is a demonstration of how an application works.]([https://youtu.be/c3QdAG8lOrQ](https://youtu.be/YS2lyf5bEGI))

<b>1. Creating ```Orders```</b><br/>
<br/>
Click on the button ```Dodaj zlecenie```and when the window appears fill in all the fields and click ```Save```. The ```Numer zlecenia``` should be 7 digits number. In every ```select box``` user should select one value.
In ```Iłość``` the value should be ```>500``` and ```3000<```. After filling all fields and selecting all necessary values from ```select boxes```, the user can save the order , which will appear in the table.
After that, user can make basic CRUD operations with the order.

<b>2. Orders's realization </b><br/>
<br/>
Click on the button ```Rozliczenia```and when the window appears, select from ```select box``` the order and int the ```Ilość zrealizowanego zlecenia``` the user should write the count of the realized order.
In the column ```Faktyczna ilość``` the user should write the actual count for every ingredient. If the count in the column ```Ilość(po obliczenich)``` is not the same as actual count, 
int the ```Straty ``` a percent discrepancy will appear and if it'll be more then ```1%```, the user should write a comment in the next column.
<br/><br/>

<b>3. Generating PDF report </b><br/>
<br/>
Click on the button ```Raportowanie```and when the window appears, select from ```select box``` the order. The user should fill all the necessary fields and select by clicking on the ```buttons```  
apropriate value for the realization process and QA parameters. After that, user can generate PDF report which will appear in the new window. 
