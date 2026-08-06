# Sistema Locadora de Carros

Aplicação Java de console, com diálogos gráficos via `JOptionPane`, para gerenciamento de uma locadora de veículos, conectada a um banco de dados **PostgreSQL**. O sistema permite cadastrar, listar, atualizar e excluir clientes, além de gerar relatórios de aluguéis e interagir com recursos avançados do banco, como *views*, *procedures* e *functions*.

## Funcionalidades

- **Cadastro de clientes**: inserção de nome, CPF, telefone, endereço, data de nascimento e CNH.
- **Listagem de clientes**: exibe todos os clientes cadastrados no banco.
- **Atualização de clientes**: altera telefone e endereço a partir do CPF.
- **Exclusão de clientes**: remove um cliente pelo CPF.
- **Relatório completo de aluguéis**: consulta uma *view* que já traz os dados de cliente, veículo e atendente unidos via `JOIN`.
- **Devolução de veículo**: chama a *stored procedure* `registrar_devolucao_veiculo`.
- **Cálculo de valores**: chama a *function* `calcular_valor_aluguel` do banco.

Toda a interação com o usuário é feita por um menu em loop, exibido através de caixas de diálogo do Swing.

## Tecnologias utilizadas

- **Java 25**
- **Maven**
- **PostgreSQL**
- **JDBC**
- **Swing** para entrada/saída de dados

## Estrutura do projeto

```
sistema-locadora/
├── pom.xml
└── src/
    └── main/
        └── java/
            ├── conectaBD/
            │   ├── ConectaPostgres.java   # Abre/fecha a conexão JDBC com o PostgreSQL
            │   ├── ClienteDAO.java        # CRUD de clientes (insert, list, update, delete)
            │   ├── AluguelDAO.java        # Relatório (view), devolução (procedure) e cálculo (function)
            │   └── testeConexaoBD.java    # Classe principal: menu interativo do sistema
            └── org/example/
                └── Main.java              # Classe de exemplo gerada pelo IntelliJ (não faz parte do fluxo principal)
```

### Descrição das classes

| Classe | Responsabilidade |
|---|---|
| `ConectaPostgres` | Estabelece e encerra a conexão com o banco via JDBC, expondo o `Statement` e a `Connection`. |
| `ClienteDAO` | Contém os métodos de acesso a dados da tabela `Cliente`: `inserirCliente`, `listarClientes`, `atualizarCliente`, `excluirCliente`. |
| `AluguelDAO` | Contém os métodos relacionados a aluguéis: `listarRelatorioView`, `realizarDevolucaoProcedure`, `calcularAlgoComFunction`. |
| `testeConexaoBD` | Ponto de entrada (`main`) da aplicação; exibe o menu e direciona as opções escolhidas para os DAOs correspondentes. |

## Pré-requisitos

- JDK 25 instalado
- Maven instalado
- PostgreSQL em execução localmente (ou acessível pela rede)
- Um banco de dados chamado `locadora_carros`, contendo:
  - Tabela `Cliente` com as colunas: `nome_cliente`, `cpf_cliente`, `telefone_cliente`, `endereco_cliente`, `data_nascimento_cliente`, `cnh_cliente`
  - View `relatorio_alugueis_completos` com as colunas: `id_aluguel`, `nome_cliente`, `modelo_veiculo`, `atendente`
  - Procedure `registrar_devolucao_veiculo(id_aluguel)`
  - Function `calcular_valor_aluguel(id_aluguel)`

> As credenciais de acesso ao banco (usuário, senha, URL) estão fixas em `testeConexaoBD.java`. Ajuste-as conforme seu ambiente antes de executar o sistema.

## Como executar

1. Clone o repositório:
   ```bash
   git clone <url-do-repositorio>
   cd sistema-locadora
   ```

2. Configure a conexão com o banco de dados em `src/main/java/conectaBD/testeConexaoBD.java`:
   ```java
   String user     = "postgres";
   String password = "1234";
   String url      = "jdbc:postgresql://localhost:5432/locadora_carros";
   ```

3. Compile o projeto com Maven:
   ```bash
   mvn clean compile
   ```

4. Execute a classe principal:
   ```bash
   mvn exec:java -Dexec.mainClass="conectaBD.testeConexaoBD"
   ```
   Ou execute diretamente pela sua IDE (IntelliJ, Eclipse etc.), rodando o método `main` da classe `testeConexaoBD`.

5. Interaja com o sistema através das caixas de diálogo exibidas.

## Menu do sistema

```
--- SISTEMA DE LOCADORA DE CARROS ---
1 - Inserir Cliente
2 - Listar Clientes
3 - Atualizar Cliente
4 - Excluir Cliente
5 - Relatório Completo (View)
6 - Devolver Veículo (Procedure)
7 - Calcular algo (Function)
0 - Sair
```

## Observações e possíveis melhorias

- As credenciais do banco estão *hardcoded* no código-fonte; recomenda-se movê-las para variáveis de ambiente ou um arquivo de configuração externo.
- Não há validação robusta de entrada (ex.: formato de CPF, data de nascimento), o que pode gerar exceções em tempo de execução.
- A classe `org.example.Main` é um exemplo padrão gerado pela IDE e não integra o fluxo funcional do sistema.
- A interface é baseada em `JOptionPane`, adequada para fins didáticos; uma evolução natural seria migrar para uma GUI completa (Swing/JavaFX) ou uma interface web.
