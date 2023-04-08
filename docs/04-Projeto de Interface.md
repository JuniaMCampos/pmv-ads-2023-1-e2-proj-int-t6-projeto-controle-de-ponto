
# Projeto de Interface

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Documentação de Especificação</a></span>

A montagem da interface do sistema foi cuidadosamente desenvolvida para proporcionar maior conforto ao usuário, para isso estamos estabelecendo foco em acessibilidade, usabilidade e agilidade, todas as telas são projetadas para funcionamento em desktops.

## Diagrama de Fluxo

Conforme pode ser visto, a *Figura 5* mostra o diagrama de fluxo de interação do usuário pelas telas do sistema. Cada uma das telas deste fluxo é detalhada na seção de Wireframes que será desenvolvido. 

|FLUXO DO USUÁRIO| DESCRIÇÃO | RF |
|--|-------------------------------------------------------|-------------------------------|
|Home Page/Login/Recupere sua Senha | O usuário tem na página inicial um breve texto mostrando para que serve o sistema e em seguida ele faz seu login na página de Acesso a Sua Conta. Caso ele tenha esquecido sua senha, é redirecionado para a página de recuperação de senha. | RF-001 |
|Página Gestor | O gestor tem acesso ao menu com as funcionalidades "Configurações", "Controle de Ponto", "Relatório" e "Solicitações"|  |
|Página Gerenciamento Pessoais Funcionários|  Nas páginas de gerenciamento do funcionário, o gestor consegue incluir um novo colaborador, excluir ou editar informações do funcionário. Para isso, ele tem as telas de cadastro de usuário, cadastro de turno e cadastro de departamento, por exemplo. | RF-007 |
|Página Gerenciamento Ponto do Funcionário| Nas páginas referente ao gerenciamento do ponto do funcionário, o gestor consegue aprovar ou desaprovar todas as modificações solicitadas pelos colaboradores, além de visualizar o ponto de cada funcionário.  | RF-006 |
|Página Gerar Relatorios | Na página de gerar relatório, o Gestor consiguirá emitir um relatório consolidado com o ponto de cada colaborador para poder enviar a contabilidade no período estabelecido.  | RF-008|
|Página Funcionário | Na página do funcionário, ele consiguirá ver o saldo do banco de horas e registrar a jornada de trabalho do dia. Também visualizará um menu com as opções: "Meu ponto", "Justificar" e "Solicitações". | RF-002 |
|Página Gerenciar Ponto| Nas páginas relacionadas ao gerenciamento do ponto, o funcionário poderá pedir ajustes, visualizar suas marcações pendentes e aprovadas | RF-003 e RF-005 |
|Página Gerenciar Justificativas | Nas páginas relacionadas ao gerenciamento de justificativas, o funcionário poderá justificar uma ausencia de marcação da jornada de trabalho e, caso seja necessário, anexar documentos. | RF-003 e RF-004|


  ![Diagrama Fluxo](img/DiagramaFluxo.PNG)


## Wireframes

Conforme fluxo de telas do projeto amostrado no item anterior, as telas do sistema são apresentadas em detalhes nos itens que se seguem. Todas essas telas têm uma estrutura comum que é apresentada na *Figura 6*. Nesta estrutura existem 3 grandes blocos, descritos a seguir. São eles:
- **Cabeçalho** - local onde são dispostos elementos fixos de identidade (logo) e icone de usuário;   
- **Conteúdo** - apresenta o conteúdo da tela em questão;   
- **Menu lateral** - apresenta o menu da aplicação que permite navegar pelas páginas.
- **Rodapé** - apresenta detalhes adicionais sobre o projeto e um mapa do site.

![](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2021-2-e2-proj-int-t2-descarte-sustentavel/blob/main/docs/img/Padrao.png)
 

## Tela - Home Page
A tela inicial permite visualizar um texto padrão apresentando o sistema para o usuário:

![Home Page](img/AcesseSuaConta.PNG)

Figura Home Page

## Tela - Login e Recuperação de Senha
O modal que se abre ao clicar no botão acessar da página Home Page, permite o usuário preencher suas credenciais conforme orientado. Além disso, o usuário pode ser direcionado para redefinir a senha da conta na opção "Esqueceu sua senha?". O modal em questão pode ser visualizado conforme figuras abaixo:

![Acesse sua Conta](img/AcesseSuaConta.PNG)

Figura Login

![Recupere sua senha](img/RecupereSenha.PNG)

## Fluxo de Telas - Cadastro de Usuário 
O fluxo de *Cadastro de Usuário* ilustrado na *Figura 9* permite ao usuário se cadastrar na aplicação caso este ainda não possua uma conta, seguindo um formulário solicitando informações obrigatórias de acordo com o tipo de perfil a ser criado.
![](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2021-2-e2-proj-int-t2-descarte-sustentavel/blob/main/docs/img/Cadastro.png)
Figura 9 – Fluxo de Cadastro de Usuário. 

## Fluxo de Telas - Esqueci a Senha  
Caso o usuário já possua uma conta mas não saiba sua senha, é permitido que ele altere sua senha por meio do fluxo apresentado na *Figura 10*. 
![](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2021-2-e2-proj-int-t2-descarte-sustentavel/blob/main/docs/img/Esqueci%20a%20Senha.png)
Figura 10 – Fluxo de Telas de Redefinição de Senha 

## Tela - Homepage 
Depois de fazer o login na aplicação, o usuário é redirecionado para uma homepage que possui a mesma estrutura da LandingPage, mas agora logado, ele possui a opção de clicar no ícone de menu ao lado da logo no cabeçalho para expandir uma barra lateral com os links para todas as páginas possíveis de serem utilizadas por ele. Além disso, ao clicar no ícone de perfil ele poderá ver informações como seu nome, tipo de usuário e fazer logout. A *Homepage* é ilustrada detalhadamente na *Figura 11*.
![](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2021-2-e2-proj-int-t2-descarte-sustentavel/blob/main/docs/img/Homepage.png)
Figura 11 – Tela Homepage

## Tela – Cadastrar Nova Solicitação
A tela de *Cadastrar Nova Solicitação*, conforme *Figura 12*, permite ao usuário solicitar o descarte de um novo material preenchendo os campos do formulário conforme solicitado e clicando em *Salvar*.
![](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2021-2-e2-proj-int-t2-descarte-sustentavel/blob/main/docs/img/SolicitacaoDescarte.png)
Figura 12 – Tela Cadastar nova Solicitação de Descarte

## Tela – Histórico de Descartes Realizados  
A *Histórico de Descartes Realizados* permite ao usuário do tipo *Descartador* visualizar todos as solicitações de descarte já realizadas, com detalhes importantes sobre elas. Essa tela segue ilustrada na *Figura 13*.
![](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2021-2-e2-proj-int-t2-descarte-sustentavel/blob/main/docs/img/Historico.png)
Figura 13 – Tela Histórico de Descartes Realizados

## Tela – Sobre   
A tela *Sobre* é ilustrada na *Figura 14* e apresenta um pouco da história do projeto, como foi idealizado, como é mantido e mostra ainda a equipe responsável pelo trabalho.   
![](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2021-2-e2-proj-int-t2-descarte-sustentavel/blob/main/docs/img/Sobre.png)
Figura 14 – Tela Sobre 

## Tela – Contato  
A tela *Contato* permite que usuário entre em contato com a equipe idealizadora, enviando uma mensagem pela caixa de texto. Essa tela pode ser vista conforme a *Figura 15*.
![](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2021-2-e2-proj-int-t2-descarte-sustentavel/blob/main/docs/img/Contato.png)
Figura 15 – Tela Contato 
 


## Wireframes

![Exemplo de Wireframe](img/wireframe-example.png)

São protótipos usados em design de interface para sugerir a estrutura de um site web e seu relacionamentos entre suas páginas. Um wireframe web é uma ilustração semelhante do layout de elementos fundamentais na interface.
 
> **Links Úteis**:
> - [Protótipos vs Wireframes](https://www.nngroup.com/videos/prototypes-vs-wireframes-ux-projects/)
> - [Ferramentas de Wireframes](https://rockcontent.com/blog/wireframes/)
> - [MarvelApp](https://marvelapp.com/developers/documentation/tutorials/)
> - [Figma](https://www.figma.com/)
> - [Adobe XD](https://www.adobe.com/br/products/xd.html#scroll)
> - [Axure](https://www.axure.com/edu) (Licença Educacional)
> - [InvisionApp](https://www.invisionapp.com/) (Licença Educacional)
