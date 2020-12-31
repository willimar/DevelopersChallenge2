
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

Para que a API funcione corretamente precisamos preparar o ambiente. Fique calmo, não será necessário instalar nada em seu sistema operacional
