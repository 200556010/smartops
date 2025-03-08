==========================================================================================================================
Scaffolding Database Tables
==========================================================================================================================

Step 1: 
	Open Package Manager Console and select "Infrastructure/Data" as the Default Project.

Step 2: 
	Run the below command in the Package Manager Console:

    Scaffold-DbContext "Server=DESKTOP-GIG3C3M\MSSQLSERVER01;Database=SmartOps;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entity -Context "SmartOpsContext" -f

Step 3: 
	After successful execution of the above command, open SMSContext class. In this class, remove the OnConfiguring method.

Step 4: Add below code before OnModelCreating method in SMSPlusContext.cs 
	partial void OnModelCreatingChild(ModelBuilder modelBuilder);

Step 5: Add in OnModelCreating method in SMSPlusContext.cs
	OnModelCreatingChild(modelBuilder);

