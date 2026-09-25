# 🧬 Nexus — Passo a passo (entregáveis)

> Cada passo tem: **o que entregar**, **o que isso melhora no projeto**, **o que você aprende/aplica de novo** e **uma dica de direção** (sem te dar a solução pronta — você monta).

---

### Passo 1 — Setup do projeto
**Entregar:** projeto console C# criado e rodando um "Hello Nexus" no terminal, com Git iniciado e primeiro commit.

**Melhora:** nada ainda, é a fundação.

**Aprende/aplica:** estrutura de um projeto .NET (`.csproj`, `Program.cs`), CLI do `dotnet`.

**Dica:** `dotnet new console` cria tudo que você precisa pra começar — não precisa configurar nada além disso agora.

---

### Passo 2 — Modelar a criatura (esqueleto)
**Entregar:** classe `Criatura` com nome, estágio de vida (enum) e os três atributos principais (fome, energia, felicidade), todos começando em valores "saudáveis".

**Melhora:** você tem um objeto de verdade pra manipular, mesmo que ainda sem comportamento.

**Aprende/aplica:** properties em C# (`get`/`set`), diferença de `public set` vs `private set` (pensa em quais atributos alguém de fora deveria poder alterar direto).

**Dica:** não se preocupe em "travar" tudo com `private set` agora — você pode refinar isso depois que tiver os métodos que alteram o estado.

---

### Passo 3 — Fome que passa com o tempo real
**Entregar:** a fome da criatura aumenta sozinha conforme o tempo real passa (não é um contador que só sobe quando você aperta um botão — é baseado em quanto tempo passou desde a última vez que você checou ela).

**Melhora:** o projeto deixa de ser "estático" e ganha a cara de tamagotchi — a criatura muda mesmo sem você estar olhando.

**Aprende/aplica:** trabalhar com data/hora em C#, e a ideia de "estado derivado" (a fome atual não é só um número salvo, é calculada a partir de um timestamp).

**Dica:** existe algo em C# que te dá "agora" (tipo o `Instant.now()` que você usaria em Java) — e existe uma forma de subtrair duas datas pra saber quanto tempo passou entre elas. Você vai guardar "quando foi a última alimentada" na criatura e calcular a fome a partir disso, em vez de só incrementar um número a cada clique.

---

### Passo 4 — Ação de alimentar
**Entregar:** um comando no menu que alimenta a criatura: reduz a fome, dá uma quantidade de XP, e atualiza o "timestamp da última alimentada" (pro cálculo do passo 3 continuar fazendo sentido).

**Melhora:** primeira interação real do usuário com a criatura — o "cuidar dela" começa a existir.

**Aprende/aplica:** métodos que alteram estado interno de um objeto, e a ideia de regra de negócio simples (ex: se a fome já tá em 0, alimentar de novo não deveria dar o XP inteiro, ou deveria ter algum limite/cooldown — pensa nisso).

**Dica:** não precisa resolver o "cooldown" com algo sofisticado agora — só pensar na regra (ex: "só ganha XP se a fome tava acima de X quando alimentou") já é o suficiente pra esse passo.

---

### Passo 5 — Energia e felicidade seguindo a mesma lógica
**Entregar:** energia caindo com o tempo (ou com ações), e ações de "descansar" e "brincar" que afetam energia/felicidade, parecido com o passo 3 e 4 mas pros outros dois atributos.

**Melhora:** a criatura fica mais "viva" — não é só um número (fome), agora tem múltiplas necessidades competindo.

**Aprende/aplica:** reaproveitar um padrão que você já escreveu (evitar copiar e colar cru — dá pra generalizar um pouco a lógica de "atributo que decai com o tempo"?).

**Dica:** pensa se faz sentido os três atributos (fome, energia, felicidade) compartilharem uma estrutura comum em vez de cada um ter sua lógica solta e duplicada — isso é uma decisão de design sua, não tem resposta única certa aqui.

---

### Passo 6 — Sistema de XP e evolução
**Entregar:** a criatura acumula XP (das ações de alimentar/brincar/etc.) e, ao cruzar certos limiares, muda de estágio evolutivo automaticamente (ex: Ovo → Filhote → Jovem → Adulto).

**Melhora:** o "gancho" principal do projeto passa a existir de verdade — é a mecânica que dá sentido a tudo o resto.

**Aprende/aplica:** enums em C# de forma mais séria, `switch` expressions (pattern matching) pra decidir a transição de estágio.

**Dica:** pensa se a evolução deveria depender só de XP acumulado, ou também do "histórico de cuidado" (ex: uma criatura super negligenciada evolui igual a uma bem cuidada?) — não precisa resolver isso perfeito agora, mas vale já deixar o design pensando nessa possibilidade pro passo 8.

---

### Passo 7 — Histórico de eventos
**Entregar:** toda ação importante (alimentar, evoluir, ficar com fome crítica) fica registrada numa lista de eventos com data/hora e descrição.

**Melhora:** dá pra "contar a história" da criatura depois — e prepara terreno pra evolução ramificada do passo 8.

**Aprende/aplica:** listas de objetos complexos (não só `List<string>`), e começar a usar LINQ pra consultar esse histórico (ex: "quantas vezes ela ficou com fome crítica").

**Dica:** LINQ tem métodos que filtram (`Where`), contam (`Count`) e pegam o mais recente (`OrderByDescending` + `First`) — você vai usar bastante isso aqui, mas descobre a sintaxe exata você mesmo, é rápido de pegar.

---

### Passo 8 — Evolução ramificada baseada no histórico
**Entregar:** o estágio evolutivo final não é único — a criatura pode evoluir "bem" ou "mal" dependendo do padrão de cuidado registrado no histórico (ex: negligência frequente leva a um caminho evolutivo diferente de cuidado constante).

**Melhora:** transforma a evolução de "barra de XP genérica" pra uma mecânica com personalidade — é o diferencial que separa esse projeto de um tutorial qualquer.

**Aprende/aplica:** lógica de negócio mais complexa combinando múltiplas condições, e possivelmente o padrão de projeto **State** (cada estágio evolutivo podendo ter seu próprio comportamento).

**Dica:** não precisa implementar o State pattern "certinho" com interfaces logo de cara — primeiro resolve com `if`/`switch` mesmo, e só refatora pra um padrão formal se sentir que o código ficou bagunçado. Refatorar depois que funciona é mais produtivo que tentar acertar a arquitetura perfeita de primeira.

---

### Passo 9 — Persistência (parar de perder tudo ao fechar)
**Entregar:** o estado da criatura (atributos, histórico, timestamps) é salvo em algum lugar e recuperado quando você abre o programa de novo.

**Melhora:** o projeto vira "de verdade" — sem isso, é só uma demo que reseta toda hora.

**Aprende/aplica:** Entity Framework Core (ORM), banco SQLite (bom pra começar por não exigir servidor instalado), migrations, async/await no acesso a dados.

**Dica:** o EF Core tem uma forma de "gerar" as tabelas do banco a partir das suas classes C# — procura por "migrations" na documentação oficial da Microsoft quando chegar aqui, é meio guiado.

---

### Passo 10 — Expor como API
**Entregar:** endpoints HTTP pra criar criatura, consultar estado, alimentar, e ver histórico — via ASP.NET Core.

**Melhora:** deixa de ser "só um script que só você roda" e vira algo que outro sistema (ou frontend futuro) poderia consumir — isso é o que mais pesa em entrevista.

**Aprende/aplica:** Web API em C#, injeção de dependência nativa do .NET, DTOs (não expor a entidade de domínio direto na resposta da API).

**Dica:** o template `dotnet new webapi` já vem com Swagger configurado — isso te dá uma telinha pra testar os endpoints sem precisar de Postman logo de cara.

---

### Passo 11 — Testes automatizados
**Entregar:** testes cobrindo as regras de evolução (a parte mais importante do sistema) e pelo menos alguns endpoints da API.

**Melhora:** prova (pra você e pra quem for ver o repo) que a lógica de negócio funciona de verdade, não só "na sorte".

**Aprende/aplica:** xUnit, a ideia de testar regra de negócio isolada da infraestrutura (banco/API).

**Dica:** comece testando só a lógica pura da `Criatura` (sem banco, sem API envolvida) — é o teste mais fácil de escrever e o mais valioso.

---

### Passo 12 — Polimento e apresentação
**Entregar:** README explicando o projeto, como rodar, prints/gif do Swagger, e (se der) um workflow simples de CI no GitHub Actions rodando os testes a cada push.

**Melhora:** transforma "projeto que funciona" em "projeto que impressiona quem olha o repositório por 2 minutos".

**Aprende/aplica:** documentação técnica, CI básico — habilidade que pesa tanto quanto código em muita vaga júnior.

**Dica:** escreve o README pensando em alguém que nunca viu o projeto — se essa pessoa consegue rodar só lendo o README, tá bom.

---

## Sobre a ordem
Dá pra pular o passo 8 (evolução ramificada) na primeira passada e voltar nele depois — os outros passos têm dependência mais forte entre si (ex: passo 4 sem o 3 não faz muito sentido). Vai testando cada passo antes de ir pro próximo, mesmo que seja só rodando no console e olhando o resultado na mão.