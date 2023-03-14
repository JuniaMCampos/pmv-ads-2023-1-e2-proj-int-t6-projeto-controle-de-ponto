# Especificações do Projeto

A definição exata do problema e os pontos mais relevantes a serem tratados neste projeto foi consolidada com a participação dos usuários em um trabalho de imersão feita pelos membros da equipe a partir da observação dos usuários em seu local natural e por meio de entrevistas. Os detalhes levantados nesse processo foram consolidados na forma de personas e histórias de usuários.

## Personas

| Laura Alves    |                                    |                       |
|--------------------|------------------------------------| ------------------------------------|
|![](https://user-images.githubusercontent.com/115122757/224854369-c0fb2df8-e118-49f7-9cb5-0d8c8e763c33.png))
|**Idade:** 28 anos -
Ocupação: Assistente Administrativo |Aplicativos: Instagram, Twitter.|


| Mateus Gomes    |                                    |                       |
|--------------------|------------------------------------| ------------------------------------|
|![](https://user-images.githubusercontent.com/115122757/224855130-92b72898-c32f-4610-a463-b9431ac246f2.png)
)
|**Idade:** 41 anos -
Ocupação: Gerente de RH |Aplicativos: Instagram, Linkedin.|





## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
| Laura Alves  | Acessar o sistema de controle de ponto  | Registrar a minha jornada de trabalho e demais eventualidades   |
| Laura Alves   | Registrar minha jornada de trabalho | Controlar meus registros de ponto e ter visibilidade do saldo em banco de horas |
| Laura Alves | Solicitar ajuste | Ajustar registros incorretos |
| Laura Alves | Solicitar abono  | Justificar ausências |
| Laura Alves | Anexar documentos | Comprovar solicitações de ajustes e abonos|
| Laura Alves | Visualizar registros pendentes e aprovados | Ter acesso ao histórico |
| Mateus Gomes| Acessar  sistema de controle de ponto | Registrar minha jornada de trabalho e demais gestão de equipe |
| Mateus Gomes| Incluir/Excluir colaborador do sistema | Manter sistema atualizado|
| Mateus Gomes| Visualizar registros de ponto dos colaboradores | Acompanhar e apurar a jornada de trabalho do colaborador |
| Mateus Gomes| Aprovar/Reprovar registros de colaboradores| Ajustar registros | 


## Requisitos

O escopo funcional do projeto é definido por meio dos requisitos funcionais que descrevem as possibilidades interação dos usuários, bem como os requisitos não funcionais que descrevem os aspectos que o sistema deverá apresentar de maneira geral. Estes requisitos são apresentados a seguir.

### Requisitos Funcionais
A tabela a seguir apresenta os requisitos do projeto, identificando a prioridade em que os mesmos devem ser entregues.

|ID    | Descrição do Requisito  | Prioridade |
|------|-----------------------------------------|----|
|RF-001| O sistema deve ser acessado por login e senha. | ALTA | 
|RF-002| O sistema deve permitir que o usuário bata o ponto na entrada, horário de pausas e saída  | ALTA |
|RF-OO3| O sistema de permitir que o usuário justifique/solicite alguma mudança na marcação do ponto| ALTA| 
|RF-004| O sistema deve permitir que o usuário anexe imagens de documentos para justificar a sua falta.| ALTA|
|RF-005| O sistema deve permitir que o usuário veja todas as suas marcações dos dias trabalhados, pendentes e aprovados.| MÉDIA|
|RF-006| O sistema deve permitir que o gestor aprove ou desaprove as marcações dos funcionários| MÉDIA|
|RF-007| O sistema deve permitir o gerenciamento dos funcionários (incluir, excluir, modificar horários de base de trabalho, etc)| ALTA|
|RF-008| O sistema deve permitir que a chefia emita um relatório com o fechamento do ponto de cada funcionário.| MÉDIA|

### Requisitos não Funcionais
A tabela a seguir apresenta os requisitos não funcionais que o projeto deverá atender.

|ID     | Descrição do Requisito  |Prioridade |
|-------|-------------------------|----|
|RNF-001| O site deve ser publicado em um ambiente acessível publicamente na Internet (Repl.it, GitHub Pages, Heroku);  | ALTA | 
|RNF-002| O site deverá ser responsivo permitindo a visualização em um celular de forma adequada |  ALTA | 
|RNF-003| O site deve ter bom nível de contraste entre os elementos da tela em conformidade | MÉDIA| 
|RNF-004| O site deve ser compatível com os principais navegadores do mercado (Google Chrome, Firefox, Microsoft Edge)| ALTA| 
|RNF-005| RNF-05	O site deve funcionar 24 horas|	ALTA|



## Restrições

As questões que limitam a execução desse projeto e que se configuram como obrigações claras para o desenvolvimento do projeto em questão são apresentadas na tabela a seguir.

|ID| Restrição                                             |
|--|-------------------------------------------------------|
|RE-01| O projeto deverá ser entregue no final do semestre letivo, não podendo extrapolar a data de 19/06/2023. |
|RE-02| O aplicativo deve se restringir às tecnologias básicas da Web no Frontend  |
|RE-03| A equipe não pode subcontratar o desenvolvimento do trabalho.|


## Diagrama de Casos de Uso

|ATOR| DESCRIÇÃO                                  |
|--|---------------------------------------------- |
|Funcionário| Pessoa responsável por registrar toda a sua rotina relacionada a jornada de trabalho.|
|Chefia| Pessoa responsável pelo gerenciamento dos funcionários e gerenciamento da jornada de trabalho desses funcionários.|

|CASOS DE USO| DESCRIÇÃO |   RF    |
|--|--------------------------------------------------------------------|------------------------------|
|Validar Usuário| O sistema autentica o ator corretamente para que ele consiga realizar suas necessidades dentro do sistema.| RF-001. |
|Gerenciar Funcionários| O ator Chefia consegue incluir e excluir funcionários, além de colocar todos os dados necessários para que o sistema funcione corretamente para o ator funcionário, como, por exemplo, o cadastro das horas em que o funcionário irá trabalhar na empresa. | RF-007.|
|Gerenciar ponto dos Funcionários| O ator Chefia consegue aprovar e desaprovar tudo que está relacionado ao ponto do ator funcionário, como por exemplo, aceitar ou recusar uma solicitação de mudança de horário de trabalho, aprovar o fechamento do ponto, etc. | RF-006.|
|Emitir Relatório de fechamento do ponto| O ator chefia, após realizar todas as alterações e inclusões relacionadas ao ponto do ator funcionário, conseguirá emitir um relatório para enviar a contabilidade para o fechamento da folha de pagamento. | RF-008.|
|Visualizar marcação dos dias trabalhados| O ator Funcionário conseguira filtrar um período especifico e visualizar sua jornada de trabalho. | RF-005.|
|Justificar mudança de horário| Caso o ator Funcionário faça algum preenchimento incorreto, ela consiga justificar essa mudança no sistema. | RF-003. |
|Informar Jornada de trabalho| O ator Funcionário consegue informar no sistema o horário que ele inicia e finaliza sua jorna de trabalho e o seu horário de almoço. | RF-002.|
|Justificar Pausas/Faltas| o ator Funcionário consegue informar um motivo de pausa /falta da jornada de trabalho, como por exemplo, exame médico, consulta médica, amamentação, doença, compensação banco de horas, luto, etc. | RF-003.|
|Anexar imagens para justificar pausas/faltas| O ator Funcionário conseguirá anexar documentos que comprovem uma pausa ou uma falta, como por exemplo, um atestado médico, um atestado de óbito, etc. | RF-004.|
|Solicitar revisão de ponto| O ator Funcionário conseguira, num período determinado pelo ator Chefia, pedir revisão do ponto caso ele perceba algum erro após a Chefia ter realizado o fechamento do ponto. | RF-003.|

|RELACIONAMENTO| DESCRIÇÃO                                 |
|--|-------------------------------------------------------|
|INCLUSÃO| É necessário fazer login no sistema para que os atores consigam realizar suas funcionalidades.|
||Para a chefia gerenciar o ponto dos funcionários, tem que ter o registro das jornadas de trabalho dos funcionários.|
||Para anexar documentos de justificativa ao sistema, o funcionário tem que solicitar a justificativa de falta/pausas.|
|EXTENSÃO| Para justificar uma pausa/falta, o usuário pode ou não pedir a solicitação de revisão de ponto, depende do prazo em que ele se encontra.|

### Representação Visual.

![](![Captura de tela 2023-03-13 201252](https://user-images.githubusercontent.com/115122757/224853603-225ee5e6-77df-4d4b-aa1f-8950613c5e68.png)

