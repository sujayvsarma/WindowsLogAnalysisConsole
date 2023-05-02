# LogAnalysisConsole
Lets you analyze IIS logs and Windows Event Logs in a super-imposed way to troubleshoot your issues quickly and efficiently.

This is a Windows GUI tool. It is designed to be run by someone who has Administrator privileges -- it does not know how to "elevate" itself, so you may run across permission issues.

## What it does

It pulls log entries from IIS Logs and the Windows Event Logs from each server specified. Then it matches the entries against the filters you set --- there is a comprehensive set of filters available (date, error codes, and so on). Whatever is left, it displays under 3 tabular grids on the screen: IIS, Event Log and one that is superimposted (IIS + Event Log matched up). 

You can also export the results to an Excel file for use elsewhere, sharing with your engineering teams and so on.

## How it helps

If your web application runs on multiple IIS servers on a "farm", it would get hard for you to narrow down the cause of errors. This tool helps you look at ALL your servers at one time and correlate errors from your IIS with your Event Logs without having to dig through the manually.

# Known issues

There is an issue with the CNV ("Convert") button on the UI. If you change the date/time and hit CNV repeatedly, the data will no longer match any particular timezone. Therefore, use the CNV button sparingly -- I have no bandwith at present to address this. You are welcome to find a patch (and share the same with me via a PR).
