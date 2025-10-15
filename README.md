# POC - CRM

Funcionalidades essenciais (MVP de CRM)
1) Negócios (Deals)
- Criar negócio


Inputs: Account, Nome, Valor (≥0), Stage inicial, CloseDate opcional.


Saídas/Efeitos: negócio no primeiro estágio; probabilidade atribuída pelo stage.


- Editar negócio


Alterar nome, valor, probabilidade (derivada do stage), close date, notas curtas.


- Mover estágio (pipeline)


Mover entre estágios lineares.


Regras:


Ganho: exige CloseDate e Valor > 0.


Perdido: exige Motivo de perda.


Efeito: recalcular probabilidade conforme stage.


- Marcar como Ganho


Define stage final “Won”, exige CloseDate; congela valor.


- Marcar como Perdido


Define stage final “Lost”; exige Motivo de perda.


- Listar e filtrar negócios


Filtros: por estágio, período, Account, probabilidade mínima, com/sem close date.


Ordenação por CloseDate, Valor, StageOrder.


- Kanban / Pipeline View


Visual por colunas (estágios) com contagem e soma de valores.


Arrastar/soltar (move stage) com validações.


- Notas do negócio (quick notes)


Adicionar/editar/remover notas textuais curtas vinculadas ao Deal.


- Histórico básico


Registrar alterações em Stage, Valor, CloseDate, Motivo de perda.


2) Empresas (Accounts)
- Criar/editar empresa


Nome (obrigatório), domínio, segmento/indústria.


- Listar/filtrar empresas


Por nome, domínio, indústria, com/sem negócios ativos.


- Resumo da empresa


Visão 360º: contatos, negócios (abertos/ganhos/perdidos), atividades pendentes.


- Vinculação por domínio


Sugerir vínculo de contatos pelo domínio do e-mail.


3) Contatos (Contacts)
Criar/editar contato


Nome, e-mail (único), telefone, cargo, empresa.


- Listar/filtrar contatos


Por empresa, e-mail, cargo, presença em negócios ativos.


- Associar contato a negócios


Adicionar/remover contato participante de um Deal.


4) Atividades (Activities)
- Criar atividade


Tipo (Task/Call/Meeting), DueDate (obrigatória), target (Deal/Contact/Account), notas.


- Concluir atividade


Exige Nota/Resultado ao concluir.


- Cancelar atividade


Exige motivo opcional.


- Listar/filtrar atividades


Por status (Pendente/Concluída/Cancelada), DueDate, target, período (agenda).


5) Relatórios e Indicadores
- Resumo do pipeline (snapshot)


Soma de valores por estágio; contagem de negócios por estágio.


- Receita (Won) por mês


Soma de negócios Ganhos agrupados por mês de CloseDate.


- Projeção simples


Soma(Valor * Probabilidade) dos negócios abertos (opcional).


6) Pesquisa, Filtros e UX de listas
- Busca rápida


Global por nome de Deal/Account/Contato e e-mail.


- Paginação e ordenação


Padrões consistentes em todas as listas.


- Exportação CSV básica


Deals filtrados, com colunas chave (para análise externa).


7) Catálogo de estágios (mesmo sem classe Stage)
Você manteve Stage “dentro” do Deal — OK. Ainda assim, o sistema precisa de um catálogo configurável para alimentar o Deal:
Gerenciar estágios (catálogo)


CRUD dos nomes de estágios, ordem, flags IsWon/IsLost, probabilidade padrão.


Mesmo que seja persistido como uma lista de configuração/lookup (sem entidade rica), as telas precisam existir.


- Reordenar estágios


Atualiza a ordem linear do pipeline.


- Mapear probabilidade por estágio


Tabela Stage → Probabilidade padrão (usada ao criar/mover Deal).


8) Integridade e Auditoria de dados (mínimo funcional)
- Campos de sistema


CreatedAt/UpdatedAt em todas as entidades.


- Arquivar (em vez de excluir) Deals/Accounts


“Inativar/Arquivar” para não perder histórico e relatórios.


- Log de mudanças de estágio/fechamento


Para explicar métricas e timeline de cada Deal.
