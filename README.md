# ThomasGreg.ClientManagement

Desenvolvido para Teste Tecnico para ThomasGreg

Para o Backend eu escolhi a estrutura de DDD com event sourcing

A vantagem da estrutura de DDD é que a aplicação cresce hozirontalmente, a complexidade de um command não deve impactar outro Command caso a complexidade da aplicação cresça um Command pode chamar livremente outro Command usando o event sourcing a partir da queue de DomainEventHolder.
Essa estrutura de event sourcing junto com o EntityFramework. Como foi pedido para usar Storage Procedures eu deveria ter incluido algum sistema de distribuição de notificações (message broker) como um Kafka, mas como não há nenhuma regra de negocio (DomainEvent) na aplicação não implementei.
A arquitetura hexagonal é implementada a partir da independencia dos serviços adicionados na aplicação no projeto de infraestrutura.

# Para execução
docker-compose up

o razor frontend estará disponivel em localhost:3000