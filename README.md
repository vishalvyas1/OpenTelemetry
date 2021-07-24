# OpenTelemetry
A sample project to demonstrate use of OpenTelemetry in .Net Framework.

# Introduction
OpenTelemetry is a powerfull open source tool for tracing. It's a vendor neutral tool hence makes it very flexible. Tracing a service includes three components.
1. Metrics generation
2. Metrics collection 
3. Visualization

In general OT is used for metric generation. The best part about OT is that it provides various options for metrics collection and visualization. OT can be configured with various exporters such as Jaegger, Zipkin, and Otlp exporter.
More elegent way to visualize the trace will be to configure OT with OT collector and let ELK read metrics from the OT collector.

This project exports traces to Jaeger UI which can be run in local enviournment.

## Jaeger UI

To build Jaeger UI execute the script jaeger-all-in-one.exe which is present the folder *jaeger-1.2.4*. After a successful execution of the file Jaeger UI can be accessed from http://localhost:16686/search. For more information one can visit [Jaeger Home Page](https://www.jaegertracing.io/).
