# Unit Testing

Unit testing is an essential part of modern software development practices. 
- It verifies business logic behavior 
- Protects from introducing unnoticed breaking changes in the future. 

Unit testing can be done for different types of functions, including orchestration client, orchestrator, and activity functions.


For example, unit testing can help in the following: 
- Avoid breaking changes as the functions can easily grow in complexity. 
- In case of Azure Durable Function the unit test will validate the logic of the HTTP trigger function for starting new orchestrations. The unit test task will be to verify the value of the Retry-After header provided in the response payload. So the unit test will mock some of IDurableClient methods to ensure predictable behavior.

In another example, the article "Set up an ethical hacking lab" explains how to set up a lab to teach ethical hacking using Azure Lab Services. In an ethical hacking class, students can learn modern techniques for defending against vulnerabilities. Penetration testing, a practice that the ethical hacking community uses, occurs when someone attempts to gain access to the system or network to demonstrate vulnerabilities that a malicious attacker may exploit. Each student gets a Windows Server host virtual machine (VM) that has two nested virtual machines: one VM with Metasploitable3 image and another VM with the Kali Linux image. You use the Metasploitable VM for exploiting purposes. The Kali VM provides access to the tools you need to execute forensic tasks.


In addition, the article "Test the self-hosted developer portal" explains how to set up unit tests and end-to-end tests for your self-hosted API Management portal. A unit test is an approach to validate small pieces of functionality. It's done in isolation from other parts of the application. An end-to-end test executes a particular user scenario taking exact steps that you expect the user to carry out. In a web application like the Azure API Management developer portal, the user scrolls through the content and selects options to achieve certain results. To replicate user navigation, you can use browser manipulation helper libraries like Puppeteer. It lets you simulate user actions and automate assumed scenarios