using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

var sistema = new SistemaSalaoBeleza();
sistema.InicializarDadosDemo();
sistema.Executar();

public class SistemaSalaoBeleza
{
    private readonly List<Cliente> _clientes = new();
    private readonly List<Profissional> _profissionais = new();
    private readonly List<Servico> _servicos = new();
    private readonly List<Agendamento> _agendamentos = new();
    private readonly List<ProdutoEstoque> _estoque = new();
    private readonly List<Pagamento> _pagamentos = new();

    public void Executar()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== SALÃO BELA VIDA - SISTEMA COMPLETO ===");
            Console.WriteLine("1. Gestão de clientes");
            Console.WriteLine("2. Gestão de profissionais");
            Console.WriteLine("3. Gestão de serviços");
            Console.WriteLine("4. Agendamentos");
            Console.WriteLine("5. Pagamentos e caixa");
            Console.WriteLine("6. Controle de estoque");
            Console.WriteLine("7. Relatórios");
            Console.WriteLine("0. Sair");

            switch (LerTexto("Escolha uma opção: "))
            {
                case "1": MenuClientes(); break;
                case "2": MenuProfissionais(); break;
                case "3": MenuServicos(); break;
                case "4": MenuAgendamentos(); break;
                case "5": MenuPagamentos(); break;
                case "6": MenuEstoque(); break;
                case "7": MenuRelatorios(); break;
                case "0": return;
                default: Mensagem("Opção inválida."); break;
            }
        }
    }

    public void InicializarDadosDemo()
    {
        _clientes.AddRange(new[]
        {
            new Cliente("Mariana Souza", "11999990001", "mariana@email.com"),
            new Cliente("João Pedro", "11999990002", "joao@email.com")
        });

        _profissionais.AddRange(new[]
        {
            new Profissional("Ana", "Cabeleireira"),
            new Profissional("Rafaela", "Manicure")
        });

        _servicos.AddRange(new[]
        {
            new Servico("Corte feminino", 80m, 60),
            new Servico("Escova", 60m, 45),
            new Servico("Manicure completa", 45m, 40)
        });

        _estoque.AddRange(new[]
        {
            new ProdutoEstoque("Shampoo profissional", 15, 35m),
            new ProdutoEstoque("Tintura castanho", 8, 42m)
        });
    }

    private void MenuClientes()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- GESTÃO DE CLIENTES ---");
            Console.WriteLine("1. Cadastrar cliente");
            Console.WriteLine("2. Listar clientes");
            Console.WriteLine("3. Editar cliente");
            Console.WriteLine("4. Remover cliente");
            Console.WriteLine("0. Voltar");

            switch (LerTexto("Opção: "))
            {
                case "1": CadastrarCliente(); break;
                case "2": ListarClientes(); break;
                case "3": EditarCliente(); break;
                case "4": RemoverCliente(); break;
                case "0": return;
                default: Mensagem("Opção inválida."); break;
            }
        }
    }

    private void CadastrarCliente()
    {
        var nome = LerTexto("Nome: ");
        var telefone = LerTexto("Telefone: ");
        var email = LerTexto("E-mail: ");
        _clientes.Add(new Cliente(nome, telefone, email));
        Mensagem("Cliente cadastrado com sucesso.");
    }

    private void ListarClientes()
    {
        Console.Clear();
        Console.WriteLine("--- CLIENTES ---");
        if (_clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
        }
        else
        {
            for (var i = 0; i < _clientes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_clientes[i]}");
            }
        }
        Pausar();
    }

    private void EditarCliente()
    {
        if (!TemItens(_clientes, "clientes")) return;
        ListarClientesSemPausa();
        var indice = LerIndice("Número do cliente para editar: ", _clientes.Count);
        var cliente = _clientes[indice];

        cliente.Nome = LerTextoOpcional($"Nome ({cliente.Nome}): ", cliente.Nome);
        cliente.Telefone = LerTextoOpcional($"Telefone ({cliente.Telefone}): ", cliente.Telefone);
        cliente.Email = LerTextoOpcional($"E-mail ({cliente.Email}): ", cliente.Email);

        Mensagem("Cliente atualizado.");
    }

    private void RemoverCliente()
    {
        if (!TemItens(_clientes, "clientes")) return;
        ListarClientesSemPausa();
        var indice = LerIndice("Número do cliente para remover: ", _clientes.Count);
        _clientes.RemoveAt(indice);
        Mensagem("Cliente removido.");
    }

    private void ListarClientesSemPausa()
    {
        Console.WriteLine("--- CLIENTES ---");
        for (var i = 0; i < _clientes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_clientes[i]}");
        }
    }

    private void MenuProfissionais()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- GESTÃO DE PROFISSIONAIS ---");
            Console.WriteLine("1. Cadastrar profissional");
            Console.WriteLine("2. Listar profissionais");
            Console.WriteLine("3. Definir comissão");
            Console.WriteLine("4. Remover profissional");
            Console.WriteLine("0. Voltar");

            switch (LerTexto("Opção: "))
            {
                case "1": CadastrarProfissional(); break;
                case "2": ListarProfissionais(); break;
                case "3": DefinirComissao(); break;
                case "4": RemoverProfissional(); break;
                case "0": return;
                default: Mensagem("Opção inválida."); break;
            }
        }
    }

    private void CadastrarProfissional()
    {
        var nome = LerTexto("Nome: ");
        var especialidade = LerTexto("Especialidade: ");
        _profissionais.Add(new Profissional(nome, especialidade));
        Mensagem("Profissional cadastrado.");
    }

    private void ListarProfissionais()
    {
        Console.Clear();
        Console.WriteLine("--- PROFISSIONAIS ---");
        if (_profissionais.Count == 0)
        {
            Console.WriteLine("Nenhum profissional cadastrado.");
        }
        else
        {
            for (var i = 0; i < _profissionais.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_profissionais[i]}");
            }
        }
        Pausar();
    }

    private void DefinirComissao()
    {
        if (!TemItens(_profissionais, "profissionais")) return;
        ListarProfissionaisSemPausa();
        var indice = LerIndice("Escolha o profissional: ", _profissionais.Count);
        var novaComissao = LerDecimal("Comissão (%) de 0 a 100: ");
        _profissionais[indice].ComissaoPercentual = Math.Clamp(novaComissao, 0m, 100m);
        Mensagem("Comissão atualizada.");
    }

    private void RemoverProfissional()
    {
        if (!TemItens(_profissionais, "profissionais")) return;
        ListarProfissionaisSemPausa();
        var indice = LerIndice("Escolha o profissional para remover: ", _profissionais.Count);
        _profissionais.RemoveAt(indice);
        Mensagem("Profissional removido.");
    }

    private void ListarProfissionaisSemPausa()
    {
        Console.WriteLine("--- PROFISSIONAIS ---");
        for (var i = 0; i < _profissionais.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_profissionais[i]}");
        }
    }

    private void MenuServicos()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- GESTÃO DE SERVIÇOS ---");
            Console.WriteLine("1. Cadastrar serviço");
            Console.WriteLine("2. Listar serviços");
            Console.WriteLine("3. Editar serviço");
            Console.WriteLine("4. Remover serviço");
            Console.WriteLine("0. Voltar");

            switch (LerTexto("Opção: "))
            {
                case "1": CadastrarServico(); break;
                case "2": ListarServicos(); break;
                case "3": EditarServico(); break;
                case "4": RemoverServico(); break;
                case "0": return;
                default: Mensagem("Opção inválida."); break;
            }
        }
    }

    private void CadastrarServico()
    {
        var nome = LerTexto("Nome do serviço: ");
        var valor = LerDecimal("Valor (R$): ");
        var duracao = LerInteiro("Duração estimada (min): ");
        _servicos.Add(new Servico(nome, valor, duracao));
        Mensagem("Serviço cadastrado.");
    }

    private void ListarServicos()
    {
        Console.Clear();
        Console.WriteLine("--- SERVIÇOS ---");
        if (_servicos.Count == 0)
        {
            Console.WriteLine("Nenhum serviço cadastrado.");
        }
        else
        {
            for (var i = 0; i < _servicos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_servicos[i]}");
            }
        }
        Pausar();
    }

    private void EditarServico()
    {
        if (!TemItens(_servicos, "serviços")) return;
        ListarServicosSemPausa();
        var indice = LerIndice("Escolha o serviço para editar: ", _servicos.Count);
        var servico = _servicos[indice];

        servico.Nome = LerTextoOpcional($"Nome ({servico.Nome}): ", servico.Nome);
        servico.Valor = LerDecimalOpcional($"Valor ({servico.Valor:C}): ", servico.Valor);
        servico.DuracaoMinutos = LerInteiroOpcional($"Duração em minutos ({servico.DuracaoMinutos}): ", servico.DuracaoMinutos);

        Mensagem("Serviço atualizado.");
    }

    private void RemoverServico()
    {
        if (!TemItens(_servicos, "serviços")) return;
        ListarServicosSemPausa();
        var indice = LerIndice("Escolha o serviço para remover: ", _servicos.Count);
        _servicos.RemoveAt(indice);
        Mensagem("Serviço removido.");
    }

    private void ListarServicosSemPausa()
    {
        Console.WriteLine("--- SERVIÇOS ---");
        for (var i = 0; i < _servicos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_servicos[i]}");
        }
    }

    private void MenuAgendamentos()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- AGENDAMENTOS ---");
            Console.WriteLine("1. Novo agendamento");
            Console.WriteLine("2. Listar agendamentos");
            Console.WriteLine("3. Confirmar atendimento");
            Console.WriteLine("4. Cancelar agendamento");
            Console.WriteLine("0. Voltar");

            switch (LerTexto("Opção: "))
            {
                case "1": NovoAgendamento(); break;
                case "2": ListarAgendamentos(); break;
                case "3": ConfirmarAtendimento(); break;
                case "4": CancelarAgendamento(); break;
                case "0": return;
                default: Mensagem("Opção inválida."); break;
            }
        }
    }

    private void NovoAgendamento()
    {
        if (!TemItens(_clientes, "clientes") || !TemItens(_profissionais, "profissionais") || !TemItens(_servicos, "serviços")) return;

        Console.WriteLine("Escolha o cliente:");
        ListarClientesSemPausa();
        var cliente = _clientes[LerIndice("Cliente: ", _clientes.Count)];

        Console.WriteLine("Escolha o profissional:");
        ListarProfissionaisSemPausa();
        var profissional = _profissionais[LerIndice("Profissional: ", _profissionais.Count)];

        Console.WriteLine("Escolha o serviço:");
        ListarServicosSemPausa();
        var servico = _servicos[LerIndice("Serviço: ", _servicos.Count)];

        var data = LerDataHora("Data e hora (dd/MM/yyyy HH:mm): ");

        _agendamentos.Add(new Agendamento(cliente, profissional, servico, data));
        Mensagem("Agendamento criado.");
    }

    private void ListarAgendamentos()
    {
        Console.Clear();
        Console.WriteLine("--- AGENDA ---");
        if (_agendamentos.Count == 0)
        {
            Console.WriteLine("Nenhum agendamento encontrado.");
        }
        else
        {
            var ordenado = _agendamentos.OrderBy(a => a.DataHora).ToList();
            for (var i = 0; i < ordenado.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ordenado[i]}");
            }
        }
        Pausar();
    }

    private void ConfirmarAtendimento()
    {
        if (!TemItens(_agendamentos, "agendamentos")) return;
        ListarAgendamentosSemPausa();
        var indice = LerIndice("Escolha o agendamento: ", _agendamentos.Count);
        var agendamento = _agendamentos[indice];
        agendamento.Status = StatusAgendamento.Concluido;

        var formaPagamento = LerTexto("Forma de pagamento (Pix, Crédito, Débito, Dinheiro): ");
        var pagamento = new Pagamento(agendamento, agendamento.Servico.Valor, formaPagamento);
        _pagamentos.Add(pagamento);

        Mensagem("Atendimento concluído e pagamento registrado.");
    }

    private void CancelarAgendamento()
    {
        if (!TemItens(_agendamentos, "agendamentos")) return;
        ListarAgendamentosSemPausa();
        var indice = LerIndice("Escolha o agendamento para cancelar: ", _agendamentos.Count);
        _agendamentos[indice].Status = StatusAgendamento.Cancelado;
        Mensagem("Agendamento cancelado.");
    }

    private void ListarAgendamentosSemPausa()
    {
        Console.WriteLine("--- AGENDAMENTOS ---");
        for (var i = 0; i < _agendamentos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_agendamentos[i]}");
        }
    }

    private void MenuPagamentos()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- PAGAMENTOS E CAIXA ---");
            Console.WriteLine("1. Listar pagamentos");
            Console.WriteLine("2. Resumo do caixa");
            Console.WriteLine("0. Voltar");

            switch (LerTexto("Opção: "))
            {
                case "1": ListarPagamentos(); break;
                case "2": ResumoCaixa(); break;
                case "0": return;
                default: Mensagem("Opção inválida."); break;
            }
        }
    }

    private void ListarPagamentos()
    {
        Console.Clear();
        Console.WriteLine("--- PAGAMENTOS ---");
        if (_pagamentos.Count == 0)
        {
            Console.WriteLine("Nenhum pagamento registrado.");
        }
        else
        {
            foreach (var pagamento in _pagamentos)
            {
                Console.WriteLine(pagamento);
            }
        }
        Pausar();
    }

    private void ResumoCaixa()
    {
        Console.Clear();
        var total = _pagamentos.Sum(p => p.Valor);
        Console.WriteLine("--- RESUMO DO CAIXA ---");
        Console.WriteLine($"Faturamento total: {total:C}");

        foreach (var grupo in _pagamentos.GroupBy(p => p.FormaPagamento))
        {
            Console.WriteLine($"{grupo.Key}: {grupo.Sum(g => g.Valor):C}");
        }

        Console.WriteLine("--- COMISSÕES ---");
        foreach (var profissional in _profissionais)
        {
            var vendas = _pagamentos
                .Where(p => p.Agendamento.Profissional == profissional)
                .Sum(p => p.Valor);

            var comissao = vendas * (profissional.ComissaoPercentual / 100m);
            Console.WriteLine($"{profissional.Nome}: {comissao:C} ({profissional.ComissaoPercentual}%)");
        }

        Pausar();
    }

    private void MenuEstoque()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- CONTROLE DE ESTOQUE ---");
            Console.WriteLine("1. Cadastrar produto");
            Console.WriteLine("2. Listar estoque");
            Console.WriteLine("3. Entrada de produto");
            Console.WriteLine("4. Saída de produto");
            Console.WriteLine("5. Alertas de estoque baixo");
            Console.WriteLine("0. Voltar");

            switch (LerTexto("Opção: "))
            {
                case "1": CadastrarProduto(); break;
                case "2": ListarEstoque(); break;
                case "3": EntradaProduto(); break;
                case "4": SaidaProduto(); break;
                case "5": AlertasEstoqueBaixo(); break;
                case "0": return;
                default: Mensagem("Opção inválida."); break;
            }
        }
    }

    private void CadastrarProduto()
    {
        var nome = LerTexto("Nome do produto: ");
        var quantidade = LerInteiro("Quantidade inicial: ");
        var custo = LerDecimal("Custo unitário (R$): ");
        _estoque.Add(new ProdutoEstoque(nome, quantidade, custo));
        Mensagem("Produto cadastrado.");
    }

    private void ListarEstoque()
    {
        Console.Clear();
        Console.WriteLine("--- ESTOQUE ---");
        if (_estoque.Count == 0)
        {
            Console.WriteLine("Estoque vazio.");
        }
        else
        {
            for (var i = 0; i < _estoque.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_estoque[i]}");
            }
        }
        Pausar();
    }

    private void EntradaProduto()
    {
        if (!TemItens(_estoque, "produtos")) return;
        ListarEstoqueSemPausa();
        var indice = LerIndice("Escolha o produto: ", _estoque.Count);
        var quantidade = LerInteiro("Quantidade de entrada: ");
        _estoque[indice].Quantidade += quantidade;
        Mensagem("Entrada registrada.");
    }

    private void SaidaProduto()
    {
        if (!TemItens(_estoque, "produtos")) return;
        ListarEstoqueSemPausa();
        var indice = LerIndice("Escolha o produto: ", _estoque.Count);
        var quantidade = LerInteiro("Quantidade de saída: ");
        _estoque[indice].Quantidade = Math.Max(0, _estoque[indice].Quantidade - quantidade);
        Mensagem("Saída registrada.");
    }

    private void AlertasEstoqueBaixo()
    {
        Console.Clear();
        Console.WriteLine("--- ALERTAS DE ESTOQUE BAIXO (<= 5) ---");
        var itens = _estoque.Where(p => p.Quantidade <= 5).ToList();
        if (itens.Count == 0)
        {
            Console.WriteLine("Nenhum item com estoque baixo.");
        }
        else
        {
            foreach (var item in itens)
            {
                Console.WriteLine(item);
            }
        }
        Pausar();
    }

    private void ListarEstoqueSemPausa()
    {
        Console.WriteLine("--- ESTOQUE ---");
        for (var i = 0; i < _estoque.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_estoque[i]}");
        }
    }

    private void MenuRelatorios()
    {
        Console.Clear();
        Console.WriteLine("--- RELATÓRIOS ---");
        Console.WriteLine($"Clientes cadastrados: {_clientes.Count}");
        Console.WriteLine($"Profissionais ativos: {_profissionais.Count}");
        Console.WriteLine($"Serviços disponíveis: {_servicos.Count}");
        Console.WriteLine($"Agendamentos totais: {_agendamentos.Count}");
        Console.WriteLine($"Agendamentos concluídos: {_agendamentos.Count(a => a.Status == StatusAgendamento.Concluido)}");
        Console.WriteLine($"Agendamentos cancelados: {_agendamentos.Count(a => a.Status == StatusAgendamento.Cancelado)}");

        var servicoPopular = _agendamentos
            .GroupBy(a => a.Servico.Nome)
            .OrderByDescending(g => g.Count())
            .Select(g => $"{g.Key} ({g.Count()}x)")
            .FirstOrDefault() ?? "Nenhum ainda";
        Console.WriteLine($"Serviço mais procurado: {servicoPopular}");

        var clienteFiel = _agendamentos
            .GroupBy(a => a.Cliente.Nome)
            .OrderByDescending(g => g.Count())
            .Select(g => $"{g.Key} ({g.Count()} agendamentos)")
            .FirstOrDefault() ?? "Nenhum ainda";
        Console.WriteLine($"Cliente mais frequente: {clienteFiel}");

        Pausar();
    }

    private static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    private static string LerTextoOpcional(string mensagem, string valorAtual)
    {
        var novoValor = LerTexto(mensagem);
        return string.IsNullOrWhiteSpace(novoValor) ? valorAtual : novoValor;
    }

    private static int LerInteiro(string mensagem)
    {
        while (true)
        {
            if (int.TryParse(LerTexto(mensagem), out var valor) && valor >= 0)
            {
                return valor;
            }
            Console.WriteLine("Digite um número inteiro válido.");
        }
    }

    private static int LerInteiroOpcional(string mensagem, int valorAtual)
    {
        var entrada = LerTexto(mensagem);
        return string.IsNullOrWhiteSpace(entrada) ? valorAtual : int.TryParse(entrada, out var valor) ? valor : valorAtual;
    }

    private static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            var texto = LerTexto(mensagem).Replace(',', '.');
            if (decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out var valor) && valor >= 0)
            {
                return valor;
            }
            Console.WriteLine("Digite um valor decimal válido.");
        }
    }

    private static decimal LerDecimalOpcional(string mensagem, decimal valorAtual)
    {
        var entrada = LerTexto(mensagem);
        if (string.IsNullOrWhiteSpace(entrada)) return valorAtual;

        entrada = entrada.Replace(',', '.');
        return decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.InvariantCulture, out var valor) ? valor : valorAtual;
    }

    private static DateTime LerDataHora(string mensagem)
    {
        while (true)
        {
            var entrada = LerTexto(mensagem);
            if (DateTime.TryParseExact(entrada, "dd/MM/yyyy HH:mm", CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None, out var data))
            {
                return data;
            }

            Console.WriteLine("Formato inválido. Use dd/MM/yyyy HH:mm");
        }
    }

    private static int LerIndice(string mensagem, int total)
    {
        while (true)
        {
            var indice = LerInteiro(mensagem);
            if (indice >= 1 && indice <= total) return indice - 1;
            Console.WriteLine("Índice fora do intervalo.");
        }
    }

    private static void Mensagem(string texto)
    {
        Console.WriteLine(texto);
        Pausar();
    }

    private static bool TemItens<T>(ICollection<T> lista, string nome)
    {
        if (lista.Count > 0) return true;
        Console.WriteLine($"Não há {nome} cadastrados.");
        Pausar();
        return false;
    }

    private static void Pausar()
    {
        Console.WriteLine("\nPressione ENTER para continuar...");
        Console.ReadLine();
    }
}

public class Cliente
{
    public Cliente(string nome, string telefone, string email)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
    }

    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }

    public override string ToString() => $"{Nome} | Tel: {Telefone} | E-mail: {Email}";
}

public class Profissional
{
    public Profissional(string nome, string especialidade)
    {
        Nome = nome;
        Especialidade = especialidade;
        ComissaoPercentual = 40m;
    }

    public string Nome { get; set; }
    public string Especialidade { get; set; }
    public decimal ComissaoPercentual { get; set; }

    public override string ToString() => $"{Nome} | {Especialidade} | Comissão: {ComissaoPercentual}%";
}

public class Servico
{
    public Servico(string nome, decimal valor, int duracaoMinutos)
    {
        Nome = nome;
        Valor = valor;
        DuracaoMinutos = duracaoMinutos;
    }

    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public int DuracaoMinutos { get; set; }

    public override string ToString() => $"{Nome} | {Valor:C} | {DuracaoMinutos} min";
}

public class Agendamento
{
    public Agendamento(Cliente cliente, Profissional profissional, Servico servico, DateTime dataHora)
    {
        Cliente = cliente;
        Profissional = profissional;
        Servico = servico;
        DataHora = dataHora;
        Status = StatusAgendamento.Agendado;
    }

    public Cliente Cliente { get; }
    public Profissional Profissional { get; }
    public Servico Servico { get; }
    public DateTime DataHora { get; }
    public StatusAgendamento Status { get; set; }

    public override string ToString() =>
        $"{DataHora:dd/MM/yyyy HH:mm} | {Cliente.Nome} | {Servico.Nome} | Prof.: {Profissional.Nome} | {Status}";
}

public enum StatusAgendamento
{
    Agendado,
    Concluido,
    Cancelado
}

public class ProdutoEstoque
{
    public ProdutoEstoque(string nome, int quantidade, decimal custoUnitario)
    {
        Nome = nome;
        Quantidade = quantidade;
        CustoUnitario = custoUnitario;
    }

    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public decimal CustoUnitario { get; set; }

    public override string ToString() => $"{Nome} | Qtd: {Quantidade} | Custo: {CustoUnitario:C}";
}

public class Pagamento
{
    public Pagamento(Agendamento agendamento, decimal valor, string formaPagamento)
    {
        Agendamento = agendamento;
        Valor = valor;
        FormaPagamento = formaPagamento;
        DataHora = DateTime.Now;
    }

    public Agendamento Agendamento { get; }
    public decimal Valor { get; }
    public string FormaPagamento { get; }
    public DateTime DataHora { get; }

    public override string ToString() =>
        $"{DataHora:dd/MM/yyyy HH:mm} | Cliente: {Agendamento.Cliente.Nome} | Serviço: {Agendamento.Servico.Nome} | {Valor:C} | {FormaPagamento}";
}
