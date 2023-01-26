# Desafio_Final_DOTNET_Codigo_Do_Futuro_G5


# Esse documento tem por objetivo explicar os testes funcionais que foram realizados na API.

## Definição dos elementos usados na API:

- **Cliente** -  pessoa física / consumidor final; 

- **Lojas** - pontos de venda (PDV);

- **Produtos** - quantidades de produtos em estoque;

- **Pedidos** - solicitações dos clientes pelos produtos.

### Descrição dos casos de teste 

Condição para os casos de teste: Loja previamente cadastrada


- **CASO 1 - Cadastrando um cliente**


Dado que ► 
A joja está na banco de dados;

Quando ► Um cliente não está cadastrado;

Então ► Realiza-se o cadastro de um novo cliente.


- **CASO 2 - Cadastro de Produto** 

Dado que ► Loja está no banco de dados;

Quando ► Não há o produto em estoque;

Então ► Lojista efetua o cadastro do produto.

- **CASO 3 - Cadastro de Pedido**

Dado que ► Loja está na banco de dados;

Quando ► Há um pedido de um cliente não cadastrado (Clientes);

Então ► Lojista efetua o cadastro do pedido.

- **CASO 4 - Cancelamento de um pedido**

Dado que ► Loja está na banco de dados;

Quando ► Um pedido de um cliente não cadastrado;

Então ► Lojista efetua o cadastro do pedido.

## Tecnologias e Ferramentas Utilizadas 

| <img src="https://static1.smartbear.co/swagger/media/assets/images/swagger_logo.svg" width="250px"> |  <img src="https://voyager.postman.com/logo/postman-logo-icon-orange.svg" width="140px"> |
|----------|----------|


## Integrantes do Grupo 5 - 💻

- [Ariane Sobreira](https://www.linkedin.com/in/ariane-sobreira-09a4a592/) 👩‍💻;
- [Gabriel Gualberto de Oliveira](https://www.linkedin.com/in/ggualberto/) 👨🏻‍💻;
- [Igor Fontes Gaspar](https://www.linkedin.com/in/igorfgaspar/) 👨🏻‍💻;
- [Leanderson Dias de Lima](https://www.linkedin.com/in/leanderson-dias-de-lima/) 👨🏾‍💻;
- [Lucas Grande](https://www.linkedin.com/in/lucas-de-grande-540111223/) 👨🏻‍💻;
- [Victor Pinheiro](https://www.linkedin.com/in/victor-sousa-pinheiro/) 👨🏻‍💻.

 


