# Metrics Conversion API

This repo contains a RESTful service built for the conversion of metric units to imperial units and vice versa


## Setting Up

- Clone the Repo
  - Restore the metric conversion postgre db (named metricconversion) located at the root folder of the application
  - Modify the db connection string (named datasettings.json) located at the APP_Data folder of the web api
  - Launch the application using the docker set up and browse the index page
  - You can call the API with the use of client tools like postman

## API End Point
- To Calculate a given metrics and vice versa you make use of the endpoint: POST: {baseurl}/api/MetricConvertion/ProcessUnitConversion
- A sample body to be used for the payload request is as given below: {
    "FromMetric" : "Farenheit",
    "ToMetric" : "Celcius",
    "MetricKey" : "FahrCels",
    "FromMetricValue" : 101
}
- A sample response of the body you get from the payload is given below:{
    "data": null,
    "message": "Conversion of Farenheit to Celcius is: 38.64 Celcius",
    "ConversionValue": 38.64,
    "statusCode": 200
}

## Usage
- There are 5 different conversions in the API and vice versa. Below are their details including their appropriate payload fields:
  - Celcius to Farenheit
    - FromMetric: Celcius
    - ToMetric: Farenheit
    - MetricKey: CelsFahr
  - Farenheit to Celcius
    - FromMetric: Farenheit
    - ToMetric: Celcius
    - MetricKey: FahrCels
  - Kilometer to Miles
    - FromMetric: Kilometer
    - ToMetric: Miles
    - MetricKey: KilometerMiles
  - Miles to Kilometer
    - FromMetric: Miles
    - ToMetric: Kilometer
    - MetricKey: MilesKilometer
  - Meters to Feet
    - FromMetric: Meters
    - ToMetric: Feet
    - MetricKey: MetersFeet
  - Feet to Meters
    - FromMetric: Feet
    - ToMetric: Meters
    - MetricKey: FeetMeters
  - Kilogram to Tons
    - FromMetric: Kilogram
    - ToMetric: Tons
    - MetricKey: TonsKilogram
  - Tons to Kilogram
    - FromMetric: Tons
    - ToMetric: Kilogram
    - MetricKey: TonsKilogram
  - Grams to Pounds
    - FromMetric: Grams
    - ToMetric: Pounds
    - MetricKey: GramsPounds
  - Pounds to Grams
    - FromMetric: Pounds
    - ToMetric: Grams
    - MetricKey: PoundsGrams

## Notes and Guides
- The "FromMetricValue" Field is the one that contains what value you are trying to convert to imperial unit or vice versa
- The guidance and usage of the payload shown above can also be seen from the DB with their rates and their formula/operators in which to calculate them.

