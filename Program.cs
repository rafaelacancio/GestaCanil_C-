using MongoDB.Driver;
using GestaoCanil;

// Cria o serviço de ligação à base de dados
MongoDBService mongoService = new MongoDBService();

// Lista que guarda os cães em memória
List<Cao> listaDeCaes = new List<Cao>();

// Array com raças disponíveis
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

// Mostra menu principal
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
    Console.WriteLine("\n1 - Adicionar cão (completo)");
    Console.WriteLine("2 - Registro rápido de cão");
    Console.WriteLine("3 - Listar cães");
    Console.WriteLine("4 - Procurar cão");
    Console.WriteLine("5 - Remover cão");
    Console.WriteLine("6 - Guardar cães na base de dados");
    Console.WriteLine("7 - Ler cães da base de dados");
    Console.WriteLine("0 - Sair");

    Console.Write("\nDigite sua opção: ");

    // Tenta converter o valor introduzido pelo utilizador (string) para um número inteiro.
    // O método TryParse evita erros (exceções) caso o utilizador digite algo inválido.
    // Se a conversão falhar, retorna false e o bloco do if será executado.
    // Caso tenha sucesso, o valor convertido é armazenado na variável 'opcao'.
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
            RegistroRapido();
            break;

        case 3:
            ListarAnimais();
            break;

        case 4:
            ProcurarAnimal();
            break;

        case 5:
            RemoverAnimal();
            break;

        case 6:
            GuardarMongoDB();
            Console.Clear();
            ExibirMenu();
            break;

        case 7:
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
        Console.WriteLine("***** Registrar um novo cão *****");

        // Gera um ID automático: se existirem cães, usa o maior ID + 1; caso contrário, começa em 1
        //operador ternário, tem if e else if
        int id = listaDeCaes.Count > 0 ? listaDeCaes.Max(a => a.Id) + 1 : 1;

        // Validação do nome
        string nome;
        do
        {
            Console.Write("Nome: ");
            nome = Console.ReadLine()!;
        }
        while (string.IsNullOrWhiteSpace(nome));

        // Validação da idade
        int idade;
        Console.Write("Idade: ");
        while (!int.TryParse(Console.ReadLine(), out idade) || idade < 0)
        {
            Console.Write("Idade inválida. Digite novamente: ");
        }

        // Validação do peso
        double peso;
        Console.Write("Peso: ");
        while (!double.TryParse(Console.ReadLine(), out peso) || peso <= 0)
        {
            Console.Write("Peso inválido. Digite novamente: ");
        }

        // Escolha da raça
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

        // Cria objeto Cao
        Cao novoCao = new Cao(id, nome, idade, peso, raca, porte);

        // Adiciona à lista
        listaDeCaes.Add(novoCao);

        Console.WriteLine("Animal registrado com sucesso!");

        // Pergunta se quer continuar
        Console.Write("\nDeseja adicionar outro cão? (s/n): ");
        string resposta = Console.ReadLine()!.ToLower();

        if (resposta != "s")
        {
            continuar = false;
        }

        Console.Clear();
    }

    VoltarMenu();
}

void RegistroRapido()
{
    Console.Clear();

    Console.WriteLine("***** Registro rápido de cão *****");

    int id = listaDeCaes.Count > 0 ? listaDeCaes.Max(a => a.Id) + 1 : 1;

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

    // Cria cão com menos dados
    Cao novoCao = new Cao(nome, idade, raca);

    // Define ID manualmente
    novoCao.Id = id;

    listaDeCaes.Add(novoCao);

    Console.WriteLine("Cão registado rapidamente com sucesso!");

    VoltarMenu();
}

void ListarAnimais()
{
    Console.Clear();
    Console.WriteLine("***** Lista de cães *****");

    if (listaDeCaes.Count == 0)
    {
        Console.WriteLine("Nenhum cão registrado.");
    }
    else
    {
        foreach (Cao animal in listaDeCaes)
        {
            Console.WriteLine(animal);
        }
    }
    VoltarMenu();
}

void ProcurarAnimal()
{
    Console.Clear();

    Console.Write("Digite o nome do cão: ");
    string nome = Console.ReadLine()!;

    // Procura na lista
    foreach (Cao animal in listaDeCaes)
    {
        //Compara o nome do animal com o nome digitado pelo utilizador, ignorando maiúsculas e minúsculas.
        if (animal.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("*****Cão encontrado*****");
            Console.WriteLine(animal);
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
            ExibirMenu();
            return;
        }
    }

    Console.WriteLine("Cão não encontrado.");
    VoltarMenu();
}

void RemoverAnimal()
{
    Console.Clear();

    Console.Write("Digite o Id do cão: ");
    int id = int.Parse(Console.ReadLine()!);//Console.ReadLine()! = “Indica que o valor não será null.”

    // Procura na lista de cães o primeiro elemento cujo Id seja igual ao valor introduzido pelo utilizador.
    // O método Find percorre a lista e aplica a condição definida pela expressão lambda (a => a.Id == id).
    // A variável 'a' representa cada elemento da lista durante a procura.
    // Se encontrar um cão com o Id correspondente, esse objeto é retornado.
    // Caso contrário, o método retorna null.
    // Por isso, a variável é declarada como 'Cao?' para indicar que pode armazenar um objeto ou null.
    Cao? animalRemover = listaDeCaes.Find(a => a.Id == id);

    if (animalRemover != null)
    {
        listaDeCaes.Remove(animalRemover);

        mongoService.GuardarAnimais(listaDeCaes);

        Console.WriteLine("Cão removido com sucesso!");
    }
    else
    {
        Console.WriteLine("Cão não encontrado.");
    }

    VoltarMenu();
}

void GuardarMongoDB()
{
    if (listaDeCaes.Count == 0)
    {
        Console.WriteLine("Não existem cães para guardar.");
        VoltarMenu();
        return;
    }

    mongoService.GuardarAnimais(listaDeCaes);

    Console.WriteLine(listaDeCaes.Count == 1 ? "1 cão guardado na base de dados."
     : $"{listaDeCaes.Count} cães guardados na base de dados.");
    VoltarMenu();
}

void LerMongoDB()
{
    listaDeCaes = mongoService.LerAnimais();

    if (listaDeCaes.Count == 0)
    {
        Console.WriteLine("Nenhum cão encontrado na base de dados.");
    }
    else if (listaDeCaes.Count == 1)
    {
        Console.WriteLine("1 cão carregado da base de dados.");
    }
    else
    {
        Console.WriteLine($"{listaDeCaes.Count} cães carregados da base de dados.");
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