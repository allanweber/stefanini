
Projeto de teste para Stefanini

Uma aplicação bem simples para listar e filtrar clientes, de acordo com as regras solicitadas.

Como nenhuma tecnologia foi exigida alêm do MVC procurei manter tudo default conforme o mvc cria o template do projeto (normalmente não uso o template).
A unica biblioteca adicionada ao projeto foi o Dapper para acesso à dados.

Optei por utilizar ajax nas requisições para não ter muito refresh de tela e requisições pesadas recarregando a model da view.

Outra opção foi não minificar os scripts para que eles possam ser lidos mais facilmente.

Mantive no commit os packages pois como não conheço a forma de correção dessa tarefa preferi manter as depências junto do fonte