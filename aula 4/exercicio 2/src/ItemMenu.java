abstract class ItemMenu {
    private String nome;
    protected float preco; // protegido para permitir acesso pelas subclasses
    private String descricao;

    public ItemMenu(String nome, float preco, String descricao) {
        this.nome = nome;
        this.preco = preco;
        this.descricao = descricao;
    }

    // Método abstrato que será implementado pelas subclasses
    public abstract void exibirDetalhes();

    // Métodos getters
    public String getNome() {
        return nome;
    }

    public float getPreco() {
        return preco;
    }

    public String getDescricao() {
        return descricao;
    }

    // Método para alterar o preço (encapsulamento)
    public void alterarPreco(float novoPreco) {
        if (novoPreco >= 0) {
            this.preco = novoPreco;
        } else {
            System.out.println("Preço não pode ser negativo.");
        }
    }
}