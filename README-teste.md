Fretefy | Projeto Técnico - Cadastro de Regiões
✅ O que foi feito

Este projeto implementa um cadastro de regiões com validações, persistência em banco de dados (SQLite), exportação para Excel e busca de cidades via API externa. A aplicação foi dividida em back-end e front-end, com as tecnologias requisitadas e atualizadas conforme necessidade.

🔄 Controle de Versão | Git

Durante o desenvolvimento, foi criada uma branch específica chamada **Develop-mateus** com o objetivo de isolar a evolução do código principal.
Essa abordagem oferece diversas vantagens no processo de versionamento:

    ✅ Isolamento de funcionalidades: alterações e testes são realizados de forma segura, sem afetar o código da branch principal (main).

    🔁 Melhor organização: facilita a gestão do projeto, separando o código estável do código em desenvolvimento.

    🧪 Ambiente seguro para testes: permite testar novas funcionalidades antes de realizar o merge para a branch principal.

    🚀 Fluxo profissional: segue as boas práticas de versionamento utilizadas em times ágeis e projetos colaborativos.

    A branch development é o ambiente de trabalho principal durante a fase de desenvolvimento, sendo posteriormente unificada à main após validação e testes.

    Não foi utilizada a branch topic neste caso, porém, em projetos pessoais, especialmente quando envolvem mais de uma pessoa, costumo usar a branch topic para ambientes de homologação (teste integrado), enquanto a branch main é reservada para produção.

        As vantagens dessa prática incluem:

        Garantir que o código em produção (main) esteja sempre estável e confiável.

        Permitir que novas funcionalidades sejam integradas e testadas no ambiente de homologação (topic) sem impactar o ambiente de produção.

        Facilitar a revisão e testes antes do deploy final, reduzindo riscos de bugs para o usuário final.



🔙 Back-End (.NET Core 3.1 + EF Core + DDD)

    API REST desenvolvida em ASP.NET Core 3.1 utilizando DDD (Domain-Driven Design).

    Persistência dos dados com Entity Framework Core, utilizando o modelo Code First com Migrations.

    Banco de dados utilizado: SQLite.

🧩 Estrutura das entidades

    O cadastro de regiões é composto por duas tabelas:

        Regiao: guarda o nome da região, seu estado (ativa/inativa) e o Id.

        RegiaoCidade: tabela de relacionamento que armazena a associação entre região e cidades, contendo os campos IdRegiao e IdCidade.

    A tabela de cidades (Cidade) é populada automaticamente na inicialização do projeto back-end, por meio de uma chamada à API oficial do IBGE.
    Esse processo garante que todas as cidades do Brasil estejam disponíveis para seleção no momento de cadastrar ou editar uma região.

🛠 Funcionalidades implementadas

    CRUD completo de regiões:

        Nome da região (único e obrigatório)

        Lista de cidades (cada uma com nome e UF)

    Regras de validação:

        Nome obrigatório e não duplicado

        Só é possivel deletar uma região apos inativala.

        Não é permitido repetir cidades dentro da mesma região (Isso é bloqueado pelo frontend)

    Ativação e desativação de regiões

    Exportação dos dados de regiões via endpoint para arquivo Excel (.xlsx)

    API para autocomplete de cidades, com base na tabela preenchida pelo IBGE

    Configuração de CORS para permitir comunicação com o front-end

🔜 Front-End (Angular 17 + Standalone Components)

    Aplicação desenvolvida com Angular 17 (utilizando componentes standalone e Angular Material).

    Funcionalidades:

        Listagem de regiões com filtros e paginação

        Cadastro e edição de regiões

        AutoComplete de cidades com busca na API

        Preenchimento automático do campo UF ao selecionar a cidade

        Validação de formulário em tempo real

        Botão de exportação para Excel

        Modal de visualização de cidades da região

🔍 Integração com a API de cidades

    O campo de cidades no formulário de regiões realiza buscas na API /api/cidade?terms=xxx, a partir do terceiro caractere digitado.

    As cidades retornadas são provenientes da tabela local de cidades, que foi previamente preenchida com dados reais do IBGE.

    Ao selecionar uma cidade, o campo UF é automaticamente preenchido com base no cadastro dessa cidade.

🚀 Como rodar o projeto
Back-End (.NET Core)

cd backend
dotnet restore
dotnet ef database update
dotnet run

    Na primeira execução, o sistema carregará todas as cidades brasileiras a partir da API do IBGE.

Front-End (Angular)

cd frontend
npm install
ng serve


Comandos para instalar o node via powershell

# Download and install fnm:
winget install Schniz.fnm

# Download and install Node.js:
fnm install 20

# Verify the Node.js version:
node -v # Should print "v20.19.2".

# Verify npm version:
npm -v # Should print "10.8.2".

Apos instalar o node basta instalar o Angular client

npm install -g @angular/cli@17

# Recomendo usar a extenção powershell 7 do vs code
# Talvez seja necessario invokar as expression do node e angular

💡 Considerações Finais

    Projeto estruturado para facilitar manutenção e evolução com base em boas práticas de back-end e front-end.

    Comunicação entre camadas feita via API REST, respeitando os padrões e verbos HTTP.

    Angular 17 com standalone components proporciona leveza e reuso de componentes.

    A utilização da base do IBGE agrega confiabilidade ao cadastro das cidades.

📫 Dúvidas ou sugestões? Entre em contato:
mateus_castanho@hotmail.com