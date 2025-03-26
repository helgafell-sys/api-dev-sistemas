class Bebida extends ItemMenu {
    private String tamanho;
    private boolean alcoolica;
    private String marca;

    public Bebida(String nome, float preco, String descricao,
                  String tamanho, boolean alcoolica, String marca) {
        super(nome, preco, descricao);
        this.tamanho = tamanho;
        this.alcoolica = alcoolica;
        this.marca = marca;
    }

    @Override
    public void exibirDetalhes() {
        System.out.println("=== BEBIDA ===");
        System.out.println("Nome: " + getNome());
        System.out.println("Descrição: " + getDescricao());
        System.out.println("Preço: R$" + getPreco());
        System.out.println("Tamanho: " + tamanho);
        System.out.println("Tipo: " + (alcoolica ? "Alcoólica" : "Não alcoólica"));
        System.out.println("Marca: " + marca);
        System.out.println("==============");
    }

    // Getters específicos
    public String getTamanho() {
        return tamanho;
    }

    public boolean isAlcoolica() {
        return alcoolica;
    }

    public String getMarca() {
        return marca;
    }
}

