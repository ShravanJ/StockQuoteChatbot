# StockQuoteChatbot
A basic demonstration of using Semantic Kernel and a local LLM for retrieving stock quotes


<img width="1646" alt="Screenshot 2025-04-26 at 9 32 16 PM" src="https://github.com/user-attachments/assets/7bee44dc-9a21-4a51-b12e-d5461747e3d3" />

# Building and Running
Before you can build and run the project, you will need the following:
* An API key for Twelve Data: https://twelvedata.com/
* LM Studio, so you can easily access a local LLM via the OpenAI API interface: https://lmstudio.ai/
* A local LLM, such as Phi3-mini: https://huggingface.co/microsoft/Phi-3-mini-128k-instruct (you can download this directly through LM Studio)

After installing LM Studio and downloading a model, go to the Developer tab to start up a Web Server. We will use the endpoint that it creates to communicate with the model for chat completion:

<img width="1762" alt="Screenshot 2025-04-26 at 9 43 24 PM" src="https://github.com/user-attachments/assets/d49275cc-8cd9-42f7-9eb8-a97a2694ae00" />

Make sure you have the correct endpoint configured in the ChatService class, and update the API key in the StockQuotePlugin class. You should be able to run the project and request quotes via the chat interface.

# How it works
This project utilizes Microsoft's Semantic Kernel package, and a concept known as function calling, to bridge an LLM's capabilities with logic inside of your C# application. You can learn more about it here: https://learn.microsoft.com/en-us/semantic-kernel/concepts/ai-services/chat-completion/function-calling/?pivots=programming-language-csharp

The Semantic Kernel will advertise the functions that we defined to the LLM, which will allow the LLM to use the output of the functions instead of its built-in inferencing. In this case, I wanted to define the capability to retrieve stock quotes via the TwelveDataSharp client library: https://github.com/pseudomarkets/TwelveDataSharp/

While I used a local LLM via LM Studio, you can interact with a wide variety of model integrations via the Semantic Kernel for chat capability: https://learn.microsoft.com/en-us/semantic-kernel/concepts/ai-services/chat-completion/chat-history?pivots=programming-language-csharp

Thanks to this excellent example by El Bruno that helped me get started with hooking up a locally run LLM to Semantic Kernel: https://github.com/elbruno/semantickernel-localLLMs

