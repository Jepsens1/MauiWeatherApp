# Weather App built in .NET Maui using OpenWeatherMap API endpoints

## Description
Very basic app that allows you to type in a city name or use current geolocation to get both current and 5 day / 3 hour forecasts

It uses openWeatherMap API endpoints to get JSON data that it then deserialized using Newtonsoft.JSON
> [!NOTE]
> JSON data from forecast endpoint each timestamp is UNIX and is converted to DateTime and then is being grouped using LINQ orderby and groupby methods

The idea behind this project was to get a deeper understanding of working with views `.xaml` converters and viewmodels using `CommunityToolKit.MVVM`
So the focus for the `Logic` project was bare minimum for getting data and generic error handling. 

## Installation Guide

```bash
git clone https://github.com/Jepsens1/MauiWeatherApp.git
```
Create your own `appsettings.json` and add a section called `APIKEY` and paste in your api key from OpenWeatherMap
