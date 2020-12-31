
# OFX Importer v1.0

Este projeto se trata de um processo de avaliação não me responsabilizo pelo uso indevido e não irei mantê-lo após o resultado.

O projeto é constituido de três camadas e mais abaixo irei descrever cada uma delas. Num situação real não seria criada uma única solução contendo todos os projetos e sim seriam separados por libs `Nuget Packages` para serem instalados e ou soluções isoladas para que possam ser tratadas de forma distinta num processo de DevOps.

Este projeto tem por finalidade simular a integração de arquivos de extrato no formato OFX, possibilitando sua importação e visualização de seu conteúdo.

### Requisitos de Sistema

* Plataforma .Net Core 3.1 para execução e compilação
* Instância de MongoDb para manipulação de dados
* Acesso a internet para baixar os pacotes do Nuget.Org

## Camada 1 `Repositório de dados`

Era requisito do processo de avaliação a gravação dos dados após a importação dos arquivos. Para tal escolhi o MongoDb.

Para possibilitar a execução da aplicação, caso não possua uma instância disponível recomendo a instalação.

A instalação pode ser feita baixando o serviço do site (MongoDb Site)[https://www.mongodb.com/try/download/community] ou criando um container docker com uma instância.

Particularmente eu prefiro a criação do Container Docker. Caso necessário o Docker para windows pode ser obtido e instalado a partir do site (Cocumentação de instalação)[https://docs.docker.com/docker-for-windows/install/].

Para criar o container você pode somente executar o seguinte comando no CMD do windows.

`docker run -d -p 27017-27019:27017-27019 --name mongodb mongo`

## Camada 2 `WebApi de integração`

Para conter a regra de negócio o domínio da aplicação achei que seria adequado criar uma API Rest.

### Publicando a API

Para que a API funcione corretamente precisamos preparar o ambiente. Fique calmo, não será necessário instalar nada em seu sistema operacional apenas iremos criar algumas variaveis de ambiente para que o sistema saiba como acessar sua base de dados.

Para que possamos criar nosso ambiente siga os passos abaixo listados:

* Abra o Windows Explorer
* Clique com o botão direito em `Meu Computador 'This PC'/Propriedades`
* Clique em `Configurações Avançadas de Sistema`
* Clique no botão `Variaveis de Ambiente`
* Em `Variaveis de Sistema` inluir os seguintes valores

| Variavel Nome       |  Valor              |  Exemplo                                                                           |
| ------------------- | ------------------- | ---------------------------------------------------------------------------------- |
|  API_DATABASEAUTH   | Authorization       |  admin                                                                             |
|  API_DATABASENAME   | Sua Base de Dados   |  nibo-test `Pode ser necessário criar`                                             |
|  API_HOSTINFO       | IP Para Acesso      |  localhost `no meu caso estava num container local`                                |
|  API_PORT           | Database Port       |  27017 `Valor padrão`                                                              |
|  API_USERNAME       | Database User       |  `No meu caso não havia`                                                           |
|  API_USERPWS        | Database Password   |  `No meu caso não havia`                                                           |
|  API_OFX_URL        | API Domain          |  https://localhost:5001 `Será usado na aplicação Client Side para encontrar a API` |

Agora você pode compilar a aplicação e executar a API.
![](img/api-image.png)
