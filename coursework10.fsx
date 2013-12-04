(*

  ITT8060 -- Advanced Programming 2013
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 10: Asyncronous computations and big data

  ------------------------------------
  Name:
  Student ID:
  ------------------------------------
 

  Answer the questions below.  You answers to questions should be
  correct F# code written after the question. This file is an F#
  script file, it should be possible to load the whole file at
  once. If you can't then you have introduced a syntax error
  somewhere.

  This coursework will be graded. When submitting the coursework,
  call the file you submit "Lastname_Firstname.fsx" with your first
  and last name appropriately, attach it to an e-mail with
  subject line "[ITT8060] Coursework 10", and send it to
  itt8060@cs.ttu.ee.

  The coursework is due on December 13.

*)

// Your overall task is to create a solution which retrieves
// statistical data from the API of the World Bank website
// http://api.worldbank.org/v2 analyses it and outputs the
// results. The key is to use the asyncronous workflows
// introduced in the lecture to perform multiple tasks in
// parallel.

// 1. Given that it is possible to download data about
//    specific indicators using the following code:

let rec getIndicatorData(date, indicator, page) = async {
  let args = [ "countries"; "indicators"; indicator ],
             [ "date", date; "page", string(page)]
  System.Console.WriteLine(worldBankUrl(args))
  let! doc = worldBankRequest args
  System.Console.WriteLine(doc)
  let pages =
    doc |> xnested [ "data" ]
    |> xattr "pages" |> int
  if (pages = page) then
    return [doc]
  else
    let page = page + 1
    let! rest = getIndicatorData(date, indicator, page)
    return doc::rest
}

let downloadAll = seq {
  for ind in [ "AG.SRF.TOTL.K2"; "AG.LND.FRST.ZS" ] do
    for year in [ "1990:1990"; "2000:2000"; "2005:2005" ] do
      yield getIndicatorData(year, ind, 1) }

let data = Async.RunSynchronously(Async.Parallel(downloadAll))

// Create a time series of data about the proportion of land
// covered with forest as a function of population growth.
// Get data points for every 5 years for 5 points prior to 2010.

// 2. Extend the error handling of worldBankDownload
// to support error messages given in XML, // like, e.g.
// <wb:error xmlns:wb="http://www.worldbank.org">
//   <wb:message id="120" key="Invalid value">The provided // parameter value is not valid</wb:message>
// </wb:error>

// 3. Display the data points for each country in text format as
// a sorted list. The sorting key should be the initial
// population of the country.


// Bonus question: You will get a bonus point for implementing 
// the whole data analysis using JSON instead of XML. World Bank
// data can be retrieved in JSON with the "format=json" value
// defined in the URL. 