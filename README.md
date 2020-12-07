# Gerenciador de Emprestimo de Jogos

- Projeto para gerenciar os emprestimos dos jogos para os amigos
- Implementação do BackEnd e FrontEnd da aplicação
- Banco configurado na porta 5433 usuario "postgres" e senha "root"

# Rodar o Docker Compose API
- docker-compose  -f docker-compose.yml up

# Rodar o Docker para frontend Vue.JS
- docker build -t gameloanmanager/vuejs-app .
- docker run -it -p 8080:80 --rm --name gameloan-manager-app-1 gameloanmanager/vuejs-app