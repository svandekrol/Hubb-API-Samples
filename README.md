# Hubb-API-Samples
The HUBB API makes use of OData conventions.  You can find more information here: [OData URL Conventions](https://www.odata.org/documentation/odata-version-3-0/url-conventions/)

## User Paging
The User Paging sample is found in Program.cs.  
It makes user of the $top and $skip parameters to page through the Users request.
Tracking the current page via the $skip parameter, and the page size via the $top parameter, this sample makes requests to the API
until the number of users returned is less than the page size ($top) parameter.