# NetCoreConsoleDemo
Template project for Console App using Dot Net Core 2.1 with Command Handler

Demo Console app project.
Contains:
- Basic Command Handler with async support
- Basic Event aggregator
- Basic error handling
- Basic logging with Serilog

SampleCommand will produce event and will publish it to the event aggregator.
Basic event aggregator will find each of the event handler and handles the event published.

SampleCommandWithAsyncException is an example of a command that will throw an exception whilst processing in async.
This application shows how it will be handled gracefully and logged it using the loggin mechanism it uses.
