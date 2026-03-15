using MongoDB.Driver;
using GestaoCanil;

MongoDBService mongoService = new MongoDBService();

List<Cao> listaDeAnimais = new List<Cao>();

string[] racas =
{
    "Indefinido",
    "Labrador",
    "Pastor Alemão",
    "Bulldog",
    "Poodle",
    "Golden Retriever",
    "Beagle",
    "Rottweiler",
    "Boxer",
    "Dálmata",
    "Cocker Spaniel"
};

ExibirMenssagem();

ExibirMenu();
void ExibirMenssagem()
{
    Console.WriteLine(@"

░██████╗░███████╗██████╗░██╗██████╗░  ░█████╗░░█████╗░███╗░░██╗██╗██╗░░░░░
██╔════╝░██╔════╝██╔══██╗██║██╔══██╗  ██╔══██╗██╔══██╗████╗░██║██║██║░░░░░
██║░░██╗░█████╗░░██████╔╝██║██████╔╝  ██║░░╚═╝███████║██╔██╗██║██║██║░░░░░
██║░░╚██╗██╔══╝░░██╔══██╗██║██╔══██╗  ██║░░██╗██╔══██║██║╚████║██║██║░░░░░
╚██████╔╝███████╗██║░░██║██║██║░░██║  ╚█████╔╝██║░░██║██║░╚███║██║███████╗
░╚═════╝░╚══════╝╚═╝░░╚═╝╚═╝╚═╝░░╚═╝  ░╚════╝░╚═╝░░╚═╝╚═╝░░╚══╝╚═╝╚══════╝

");
}

void ExibirMenu()
{
    Console.WriteLine("\n1 - Adicionar animal");
    Console.WriteLine("2 - Listar animais");
    Console.WriteLine("3 - Procurar animal");
    Console.WriteLine("4 - Remover animal");
    Console.WriteLine("5 - Guardar animais na base de dados");
    Console.WriteLine("6 - Ler animais da base de dados");
    Console.WriteLine("0 - Sair");

    Console.Write("\nDigite sua opção: ");

    if (!int.TryParse(Console.ReadLine(), out int opcao))
    {
        Console.WriteLine("Opção inválida.");
        ExibirMenu();
        return;
    }

    switch (opcao)
    {
        case 1:
            AdicionarAnimal();
            break;

        case 2:
            ListarAnimais();
            break;

        case 3:
            ProcurarAnimal();
            break;

        case 4:
            RemoverAnimal();
            break;

        case 5:
            GuardarMongoDB();
            Console.Clear();
            ExibirMenu();
            break;

        case 6:
            LerMongoDB();
            Console.Clear();
            ExibirMenu();
            break;

        case 0:
            Environment.Exit(0);
            break;

        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}

void AdicionarAnimal()
{
    Console.Clear();

    bool continuar = true;

    while (continuar)
    {
        Console.WriteLine("***** Registrar um novo animal *****");

        int id = listaDeAnimais.Count > 0 ? listaDeAnimais.Max(a => a.Id) + 1 : 1;

        string nome;
        do
        {
            Console.Write("Nome: ");
            nome = Console.ReadLine()!;
        }
        while (string.IsNullOrWhiteSpace(nome));

        int idade;
        Console.Write("Idade: ");
        while (!int.TryParse(Console.ReadLine(), out idade) || idade < 0)
        {
            Console.Write("Idade inválida. Digite novamente: ");
        }

        double peso;
        Console.Write("Peso: ");
        while (!double.TryParse(Console.ReadLine(), out peso) || peso <= 0)
        {
            Console.Write("Peso inválido. Digite novamente: ");
        }

        Console.WriteLine("Escolha uma raça:");

        for (int i = 0; i < racas.Length; i++)
        {
            Console.WriteLine($"{i} - {racas[i]}");
        }

        int escolha;
        while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 0 || escolha >= racas.Length)
        {
            Console.Write("Escolha inválida. Digite novamente: ");
        }

        string raca = racas[escolha];

        Console.Write("Porte: ");
        string porte = Console.ReadLine()!;

        Cao novoCao = new Cao(id, nome, idade, peso, raca, porte);

        listaDeAnimais.Add(novoCao);

        Console.WriteLine("Animal registrado com sucesso!");

        Console.Write("\nDeseja adicionar outro animal? (s/n): ");
        string resposta = Console.ReadLine()!.ToLower();

        if (resposta != "s")
        {
            continuar = false;
        }

        Console.Clear();
    }

    VoltarMenu();
}

void ListarAnimais()
{
    Console.Clear();
    Console.WriteLine("***** Lista de animais *****");

    if (listaDeAnimais.Count == 0)
    {
        Console.WriteLine("Nenhum animal registrado.");
    }
    else
    {
        foreach (Cao animal in listaDeAnimais)
        {
            Console.WriteLine("******************");
            Console.WriteLine(animal);
        }
    }
    VoltarMenu();
}

void ProcurarAnimal()
{
    Console.Clear();

    Console.Write("Digite o nome do animal: ");
    string nome = Console.ReadLine()!;

    foreach (Cao animal in listaDeAnimais)
    {
        if (animal.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Animal encontrado:");
            Console.WriteLine(animal);
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
            ExibirMenu();
            return;
        }
    }

    Console.WriteLine("Animal não encontrado.");
    VoltarMenu();
}

void RemoverAnimal()
{
    Console.Clear();

    Console.Write("Digite o Id do animal: ");
    int id = int.Parse(Console.ReadLine()!);

    Cao? animalRemover = listaDeAnimais.Find(a => a.Id == id);

    if (animalRemover != null)
    {
        listaDeAnimais.Remove(animalRemover);

        // Atualiza MongoDB
        mongoService.GuardarAnimais(listaDeAnimais);

        Console.WriteLine("Animal removido com sucesso!");
    }
    else
    {
        Console.WriteLine("Animal não encontrado.");
    }

    VoltarMenu();
}

void GuardarMongoDB()
{
    if (listaDeAnimais.Count == 0)
    {
        Console.WriteLine("Não existem animais para guardar.");
        VoltarMenu();
        return;
    }

    mongoService.GuardarAnimais(listaDeAnimais);

    //operador ternário para exibir a mensagem correta
    Console.WriteLine(listaDeAnimais.Count == 1 ? "1 animal guardado na base de dados."
    : $"{listaDeAnimais.Count} animais guardados na base de dados."
);
    VoltarMenu();
}

void LerMongoDB()
{
    listaDeAnimais = mongoService.LerAnimais();

    if (listaDeAnimais.Count == 0)
    {
        Console.WriteLine("Nenhum animal encontrado na base de dados.");
    }
    else if (listaDeAnimais.Count == 1)
    {
        Console.WriteLine("1 animal carregado da base de dados.");
    }
    else
    {
        Console.WriteLine($"{listaDeAnimais.Count} animais carregados da base de dados.");
    }

    ListarAnimais();
    VoltarMenu();
}

void VoltarMenu()
{
    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
    ExibirMenu();
}