# Coisas Emprestadas

Aplicação desktop em C# (WinForms) para controlar itens que você empresta para amigos, com armazenamento dos dados em banco de dados MariaDB/MySQL.

- Registra **item**, **data do empréstimo**, **contato do amigo** e, opcionalmente, a **data combinada de devolução**.
- A tela inicial lista os empréstimos e destaca em vermelho os que passaram da data combinada.
- Ao devolver, o sistema grava a data real da devolução.
- Os dados ficam salvos no banco e reaparecem na próxima vez que você abrir o programa.

---

## 1. Requisitos

| Ferramenta | Versão usada | Observação |
|---|---|---|
| .NET SDK | 10 | `dotnet --version` deve responder |
| Visual Studio | 2026 (v18) Community | Com a carga de trabalho **Desenvolvimento para desktop com .NET** |
| XAMPP | com MySQL/MariaDB | Servidor de banco de dados local |
| HeidiSQL (opcional) | qualquer | Só para visualizar os dados |

> O banco usado é o **MySQL/MariaDB do XAMPP**, que roda em `localhost:3306`. O HeidiSQL é apenas um programa para visualizar/editar o banco — ele **não** é o servidor.

---

## 2. Estrutura da solução

```
CoisasEmprestadas.slnx
├── CoisasEmprestadas.UI        → Interface gráfica (formulários WinForms)
├── CoisasEmprestadas.Negocio   → Modelo e regras de negócio
└── CoisasEmprestadas.Dados     → Acesso ao banco (MySqlConnector)
```

Cada camada só conversa com a de baixo: a **UI** chama o **Negócio**, que chama os **Dados**. Não existe código SQL dentro dos formulários.

---

## 3. Passo a passo para rodar (bem detalhado)

> **Antes de começar — não confunda os programas:**
> - **Visual Studio Community** é o programa grande, gratuito, que tem o **designer visual** das telas. É o que vamos usar. (Não existe "VS Code Community".)
> - **VS Code** é outro programa, um editor leve, **sem** designer. A alternativa pelo VS Code está no final desta seção.

### Passo 1 — Ligue o servidor de banco (XAMPP)

O programa precisa que o **MySQL** esteja ligado, senão não conecta.

1. Abra o **XAMPP Control Panel** (`C:\xampp\xampp-control.exe`).
2. Na linha **MySQL**, clique no botão **Start**.
3. O nome **MySQL** deve ficar **verde** e aparecer uma porta `3306` ao lado.

   Alternativa por atalho: dê duplo clique em `C:\xampp\mysql_start.bat`.

> Deixe o XAMPP Control Panel aberto enquanto usar o programa. Se fechar o MySQL, o programa perde a conexão.

### Passo 2 — Abra o programa Visual Studio

1. Clique no menu **Iniciar** do Windows e digite `Visual Studio`.
2. Clique em **Visual Studio 2026** (o ícone de "estrela" roxa). Aguarde abrir — na primeira vez demora um pouco e pode pedir login; escolha **"Talvez depois"** / **"Continuar sem entrar"**.
3. A **janela inicial** aparece com uma lista de opções. Clique em **Abrir um projeto ou solução** ("Open a project or solution").

### Passo 3 — Abra a solução do projeto

1. Na janela que abrir, navegue até a pasta **`E:\Ferramentas_Visuais`**.
2. Selecione o arquivo **`CoisasEmprestadas.slnx`** e clique em **Abrir**.
3. Aguarde alguns segundos enquanto o Visual Studio carrega os 3 projetos.

> Se você não achar o arquivo, na caixa "Nome do arquivo" digite `*.slnx` e pressione Enter.

### Passo 4 — Conheça a tela do Visual Studio

Depois de abrir, você verá:

- À direita, o **Gerenciador de Soluções** (Solution Explorer): uma árvore com `CoisasEmprestadas.UI`, `CoisasEmprestadas.Negocio` e `CoisasEmprestadas.Dados`. São as 3 camadas.
- No topo, uma barra verde com o botão **▶** e o nome de um projeto. É por ali que você inicia o programa.
- No centro, a área de edição (onde abre o código quando você dá duplo clique em um arquivo).

Se o **Gerenciador de Soluções** não aparecer, use o menu **Exibir → Gerenciador de Soluções** (ou `Ctrl+Alt+L`).

### Passo 5 — Defina o projeto que vai iniciar

Precisamos dizer ao Visual Studio para iniciar a **interface** (`CoisasEmprestadas.UI`).

1. No **Gerenciador de Soluções**, encontre o projeto **CoisasEmprestadas.UI**.
2. Clique nele com o **botão direito** → **Definir como Projeto de Inicialização**.
3. Confira: o nome **CoisasEmprestadas.UI** deve ficar em **negrito**, e o botão **▶** no topo deve mostrar **CoisasEmprestadas.UI**.

### Passo 6 — Confira a conexão com o banco

1. No **Gerenciador de Soluções**, expanda **CoisasEmprestadas.UI** (clique na setinha ▶ à esquerda).
2. Dê **duplo clique** no arquivo **`App.config`**.
3. Verifique a linha da *connection string*:

```xml
<connectionStrings>
  <add name="CoisasEmprestadas"
       connectionString="Server=localhost;Port=3306;Database=coisas_emprestadas;User ID=root;Password=123456;SslMode=None;AllowUserVariables=True;"
       providerName="MySqlConnector" />
</connectionStrings>
```

- `User ID` / `Password` devem ser o usuário e a senha do seu MySQL do XAMPP.
- Se o seu XAMPP usar `root` **sem** senha, apague o trecho `Password=123456;`.
- A senha fica **somente neste arquivo**, nunca no código-fonte.

### Passo 7 — Execute o programa

1. Pressione a tecla **F5** (ou clique no botão verde **▶ CoisasEmprestadas.UI** no topo).
2. Na primeira vez, o Visual Studio **compila** o projeto (barra de status embaixo). Pode demorar ~30 segundos.
3. A janela **"Coisas Emprestadas"** deve abrir no centro da tela.

> O **F5** roda em "modo depuração" (bom para achar erros). Se preferir, use **Ctrl+F5** para rodar sem depuração.

### Passo 8 — Use e feche

- Para incluir um empréstimo: botão **Novo empréstimo** (ou `Alt+N`).
- Para fechar o programa: botão **Sair**, tecla `Esc`, ou o **X** da janela.
- Para **parar a execução** no Visual Studio: menu **Depurar → Parar Depuração** (ou `Shift+F5`).

Pronto! Na primeira execução o programa **cria automaticamente** o banco `coisas_emprestadas` e as tabelas `Emprestimo` e `LogOperacao` — você não precisa criar nada à mão.

### Alternativa: rodar pelo VS Code (sem designer)

Se você quiser apenas **executar** sem editar telas visualmente:

1. Ligue o MySQL no XAMPP (Passo 1 acima).
2. Abra o **VS Code** → menu **Arquivo → Abrir Pasta** → escolha **`E:\Ferramentas_Visuais`**.
3. Abra o terminal integrado: menu **Terminal → Novo Terminal** (ou `Ctrl+'`).
4. Digite o comando e pressione Enter:

   ```powershell
   dotnet run --project CoisasEmprestadas.UI
   ```

5. A janela do programa abre normalmente. Para parar, feche a janela do programa (ou `Ctrl+C` no terminal).

> No VS Code não existe o designer de telas; para **editar** os formulários visualmente, use o Visual Studio Community.
8
---

## 4. Como usar a aplicação

| Ação | Como fazer |
|---|---|
| Incluir um empréstimo | Botão **Novo empréstimo** (ou `Alt+N`) |
| Registrar a devolução | Selecione a linha e clique **Registrar devolução** (ou `Alt+D`), ou **dê duplo clique** na linha |
| Recarregar a lista | **Atualizar** (ou tecla `F5`) |
| Fechar | **Sair** (ou tecla `Esc`) |

Na janela de novo empréstimo, os campos **Item** e **Amigo** são obrigatórios; o **Contato** (telefone) e a **data combinada de devolução** são opcionais. Datas inválidas são apontadas pelo ícone de erro ao lado do campo.

Os empréstimos **em atraso** aparecem destacados em vermelho com a quantidade de dias de atraso.

---

## 5. (Opcional) Criar as tabelas manualmente

O programa cria a estrutura sozinho, mas se preferir executar o script à mão, use o arquivo:

```
CoisasEmprestadas.Dados\Scripts\banco.sql
```

No HeidiSQL: conecte no servidor `localhost` (usuário `root`), abra uma aba **Consulta**, cole o conteúdo de `banco.sql` e execute.

---

## 6. Problemas comuns

| Mensagem / sintoma | Causa provável | Solução |
|---|---|---|
| "Não foi possível conectar ao servidor de banco de dados" | MySQL do XAMPP parado | Ligue o **MySQL** no XAMPP Control Panel |
| "Acesso negado ao banco de dados" | Usuário/senha errados | Corrija `User ID`/`Password` no `App.config` |
| "O banco de dados informado não existe" | Nome do banco diferente | Confira `Database=` no `App.config` |
| A lista aparece vazia | Nenhum empréstimo cadastrado | Normal na primeira vez; clique em **Novo empréstimo** |
| `dotnet` não reconhecido no terminal | SDK não instalado/no PATH | Instale o .NET SDK 10 |

---

## 7. Decisões técnicas (para avaliação)

- **Separação de camadas (SoC):** UI, Negócio e Dados em projetos separados; a UI não contém SQL nem regras complexas.
- **Acesso a dados:** conexões abertas/fechadas com `using` (ciclo de vida correto); todos os comandos usam **parâmetros** (`@Item`, `@Id`, ...), evitando **SQL Injection**.
- **Atomicidade:** cadastro e devolução gravam também em `LogOperacao` dentro de uma **transação** (`MySqlTransaction`), com `Commit` no sucesso e `Rollback` em caso de erro.
- **Validação:** formato na interface (ErrorProvider, máscaras, `DateTime.TryParseExact`) e regras de negócio antes de enviar ao banco (datas, campos obrigatórios, limites).
- **Exceções:** erros de banco (`MySqlException`) e de regra (`RegraNegocioException`) tratados de forma específica, com mensagens amigáveis e **sem exibir a pilha de erros**.
- **Segurança/Configuração:** *connection string* somente no `App.config`.
- **Usabilidade:** atalhos de teclado (`Alt+N`, `Alt+D`, `F5`, `Esc`, `Enter`/`Esc` nos diálogos), ordem de tabulação (`TabIndex`) e foco inicial definidos.
