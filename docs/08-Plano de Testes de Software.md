# Plano de Testes de Software

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>

Apresente os cenários de testes utilizados na realização dos testes da sua aplicação. Escolha cenários de testes que demonstrem os requisitos sendo satisfeitos.


Por exemplo:
 
| **Caso de Teste** 	|CT 01 – Logar no sistema|
|-------	| ----------------	|
|	Requisito Associado 	| RF001 - O sistema deve ser acessado por login e senha |
| Objetivo do Teste 	| Permitir que o usuário acesse a plataforma através do login e senha. |
| Passos 	| -1.	Acessar o navegador <br> - 2.	Acessar o endereço do site <br> - 3.	Clicar no botão Acesse <br> - 4.	Inserir o Usuário ID <br> - 5.	Inserir a Senha <br> - 	6.	Clicar no botão Acessar |
|Critério de Êxito | - O usuário deve ser autenticado corretamente e deve ter acesso ao sistema de acordo com as permissões a ele concedidas. |


| **Caso de Teste** 	|**CT 02 - Registrar o ponto**	|
|-------| -----------------|
|Requisito Associado | RF002 - O sistema deve permitir que o usuário bata o ponto na entrada, horário de pausas e saída |
| Objetivo do Teste 	| Registrar a jornada de trabalho. |
| Passos 	| - 1.	Acessar o navegador <br> - 2.	Acessar o endereço do site <br> - 3.	Clicar no botão Acesse <br> - 4.	Inserir o Usuário ID <br> - 5.	Inserir a Senha <br> - 6.	Clicar no botão Acessar <br>- 7.	Clicar no botão Bater ponto para registrar a entrada, pausas e saída |
|Critério de Êxito | -O sistema deve registrar os horários de ponto batidos pelo usuário. |


| **Caso de Teste** 	| **CT 03 - Justificar registro de ponto incorreto, falta e solicitar abono**	|
|-------| -----------------|
|Requisito Associado | RF003 - O sistema de permitir que o usuário justifique/solicite alguma mudança na marcação do ponto
||RF004 - O sistema deve permitir que o usuário anexe imagens de documentos para justificar a sua falta.|
| Objetivo do Teste 	| Usuário pode justificar registros de ponto incorretos e pode anexar documentos para comprovar sua justificativa, seja ela por falta, abono, atestado, atraso etc... |
| Passos 	| - 1.	Acessar o navegador <br> - 2.	Acessar o endereço do site <br> - 3.	Clicar no botão Acesse <br> - 4.	Inserir o Usuário ID <br> - 5.	Inserir a Senha <br> - 6.	Clicar no botão Acessar <br> - 7.	Clicar no botão Justificar <br> -8.	Clicar no botão Anexar <br> -9.	Clicar no botão Enviar|
|Critério de Êxito | -O sistema deve exibir o feedback ‘Alterações realizadas com sucesso!’ e deve registrar a justificativa do usuário.|


| **Caso de Teste** | **CT 04 – Visualizar registros de ponto**	|
|-------| -----------------|
|Requisito Associado |RF005 - O sistema deve permitir que o usuário veja todas as suas marcações dos dias trabalhados, pendentes e aprovados.|
| Objetivo do Teste 	| O usuário pode visualizar seus registros de ponto realizados e não realizados. |
| Passos 	| - 1.	Acessar o navegador <br> - 2.	Acessar o endereço do site <br> - 3.	Clicar no botão Acesse <br> - 4.	Inserir o Usuário ID <br> - 5.	Inserir a Senha <br> - 6.	Clicar no botão Acessar <br> - 7.		Clicar em Ver ponto|
|Critério de Êxito | -O sistema deve exibir uma lista contendo todos os registros de ponto computados para aquele usuário como também os dias em que não foram computados.|


| **Caso de Teste** 	| **CT 05 – Aprovar/ Reprovar ajustes**	|
|-------| -----------------|
|Requisito Associado |RF006 - O sistema deve permitir que o gestor aprove ou desaprove as marcações dos funcionários|
| Objetivo do Teste 	| Gerenciar solicitações de ajustes dos funcionários. |
| Passos 	| - 1.	Acessar o navegador <br> - 2.	Acessar o endereço do site <br> - 3.	Clicar no botão Acesse <br> - 4.	Inserir o Usuário ID <br> - 5.	Inserir a Senha <br> - 6.	Clicar no botão Acessar <br>- 7.	Clicar em Solicitações|
|Critério de Êxito | -Ao aprovar uma solicitação, sistema deve exibir o feedback ‘Solicitação aprovada com sucesso!’.|


| **Caso de Teste** 	| **CT 06 – Gerenciar funcionários** |
|-------| -----------------|
|Requisito Associado |RF007 - O sistema deve permitir o gerenciamento dos funcionários (incluir, excluir, modificar horários de base de trabalho, etc)|
| Objetivo do Teste 	| Manter sistema atualizado, incluindo, atualizando e removendo funcionários. |
| Passos 	| - 1.	Acessar o navegador <br> - 2.	Acessar o endereço do site <br> - 3.	Clicar no botão Acesse <br> - 4.	Inserir o Usuário ID <br> - 5.	Inserir a Senha <br> - 6.	Clicar no botão Acessar <br> - 7.	Clicar em Configurações |
|Critério de Êxito | -Ao incluir funcionário, sistema deve exibir o feedback ‘Inclusão realizada com sucesso!’ e deve incluí-lo no sistema. - Ao excluir funcionário, sistema deve exibir o feedback ‘Exclusão realizada com sucesso!’ e deve removê-lo do sistema. -Ao modificar horários, sistema deve exibir o feedback ‘Atualização realizada com sucesso!’ e deve atualizar os horários.|


| **Caso de Teste** 	| **CT 07 – Emitir relatórios** |
|-------| -----------------|
|Requisito Associado |RF008 - O sistema deve permitir que a chefia emita um relatório com o fechamento do ponto de cada funcionário.|
| Objetivo do Teste 	| Enviar relatório para contabilidade para fechamento da folha do ponto. |
| Passos 	| - 1.	Acessar o navegador <br> - 2.	Acessar o endereço do site <br> - 3.	Clicar no botão Acesse <br> - 4.	Inserir o Usuário ID <br> - 5.	Inserir a Senha <br> - 6.	Clicar no botão Acessar <br>- 7.	Clicar em Relatórios |
|Critério de Êxito | -O relatório deve ser em formato pdf e  pode ser extraído.|
