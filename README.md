# Meu Desenho 

## O que é
"Meu Desenho" é uma aplicação UWP que utiliza AI para fazer reconhecimento de desenhos.
A aplicação tem uma tela onde você pode fazer um desenho utilizando o mouse ou uma caneta e pedir para o sistema tentar reconhecer.
Esse reconhecimento é feito utilizando um modelo criado no [Azure Custom Vision](https://www.customvision.ai/).

## Como funciona
O reconhecimento dos desenhos pode ser feito de duas maneiras: On-Line e Off-Line.

### Off-Line
Uma das coisas mais legais do [Azure Custom Vision](https://www.customvision.ai/) é a possibilidade de exportar o modelo gerado e utilizar nas suas aplicações. Mais informações [aqui](https://docs.microsoft.com/pt-br/azure/cognitive-services/custom-vision-service/export-your-model).
No nosso caso, o modelo foi exportado como [ONNX](https://onnx.ai/) para ser utilizado com o [Windows ML](https://docs.microsoft.com/pt-br/windows/ai/windows-ml/).
Você pode encontrar o modelo criado [aqui](https://github.com/angelobelchior/MeuDesenho/blob/master/MeuDesenho/Assets/Models/MeuDesenho.onnx).
Ao baixar esse modelo e colocar dentro na dua aplicação UWP, automáticamente o Visual Studio 2019 gera uma classe para consumo do mesmo.
Essa [classe](https://github.com/angelobelchior/MeuDesenho/blob/master/MeuDesenho/Assets/Models/MeuDesenho.cs) contém o carregamento do arquivo ONNX um [método](https://github.com/angelobelchior/MeuDesenho/blob/76d70a4b053ba952a2825cadd886c523c91d0271/MeuDesenho/Assets/Models/MeuDesenho.cs#L30) para fazer o reconhecimento do desenho. Esse método retorna as Tags e percentual de acerto de cada uma.

### On-Line
O [Azure Custom Vision](https://www.customvision.ai/) disponibiliza [API's](https://docs.microsoft.com/pt-br/azure/cognitive-services/custom-vision-service/) para consumo do serviço. Basicamente, nós enviamos uma imagem ao serviço e o mesmo retorna uma estrura JSON contendo as Tags e seus respectivos percentuais de acerto.
Para utilizar o modo On-Line será necessário informar na classe de [parâmetros](https://github.com/angelobelchior/MeuDesenho/blob/master/MeuDesenho/Infra/Parameters.cs	) o [endpoint](https://github.com/angelobelchior/MeuDesenho/blob/76d70a4b053ba952a2825cadd886c523c91d0271/MeuDesenho/Infra/Parameters.cs#L5) e [chave de acesso](https://github.com/angelobelchior/MeuDesenho/blob/76d70a4b053ba952a2825cadd886c523c91d0271/MeuDesenho/Infra/Parameters.cs#L6). Essa informações você pode obter no site do [Azure Custom Vision](https://www.customvision.ai/). Mais informações [aqui](https://docs.microsoft.com/pt-br/azure/cognitive-services/custom-vision-service/use-prediction-api).

Porém, antes disso é necessário treinar seu modelo adicionando imagens de desenho. 
Eu estou exportando as imagens que estão no meu projeto dentro do [Azure Custom Vision](https://www.customvision.ai/) e assim que finalizar subo aqui no github.
