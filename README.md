**This project is under active development. The event capture/query may have bugs, and the dashboard is not fully functional.** 

# FasTnT
FasTnT is a C# implementation of GS1 EPCIS 1.2 standard. [https://www.gs1.org/epcis](https://www.gs1.org/epcis)

## Setup

1. Install PostGreSQL if needed and create a new user/database for FasTnT;
2. Update the `connectionStrings.shared.config` file with your connection string;
3. Download, extract and build the source code from Github (using `Debug` configuration);
4. Run the `.\tools\Up.cmd` utility. It will apply the migrations to the database;
5. Set `FasTnT.Web` project as startup project, and start the solution

That's it. You should now have a full EPCIS repository working on your computer.

## Endpoints

### EPCIS endpoints:

- Event capture: `http://localhost:12008/Services/1.2/EpcisCapture/` 
- Queries : `http://localhost:12008/Services/1.2/EpcisQuery/`

The file `tools\EPCIS_Samples.postman_collection.json` contains sample of HTTP queries that you can perform on FasTnT (import and run in [PostMan](https://www.getpostman.com/))

### Web Dashboard

**FasTnT** also contains an embedded web dashboard that allows you to manage your server, Master Data and allowed users. The default URL is `http://localhost:12008/`.

The default username/password for the dashboard is `admin`/`p@ssw0rd`.


\* Note that the base URL or port number may change due to your IIS configuration.

----------

## Help/contact

FasTnT is developed and maintained by Louis-Axel Ambroise. If you like this work, have a question or just want to contact me, feel free to send me a mail on the address specified in my [GitHub profile](http://github.com/louisaxel-ambroise).