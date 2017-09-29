Most of this exercise should be quite routine to you, except it will require you to create a new UOM named "millibar" that 
belongs to the "Pressure" UOMClass.

Here is the order to process the 3 files associated with this exercise:

1. PIPointsForPIAFSDKWorkshop.xlsx > Use the Excel add-in PI Builder to create the PIPoints found in the spreadsheet.
2. millibar.xml > Use PI System Explorer to import the UOM into your UOMDatabase.
3. Weather Exercise 1.xml > Use PI System Explorer to import this into a new database named "Weather".

About the millibar UOM:

If you want to create this UOM manually, you will need to:
1. Go to the UOMDatabase bar in lower-left of PSE.
2. Navigate to the "Pressure" UOMClass.
3. Add a new UOM.  Name it "millibar".  Have its reference UOM be "bar".  Use a Factor of 0.001. 

