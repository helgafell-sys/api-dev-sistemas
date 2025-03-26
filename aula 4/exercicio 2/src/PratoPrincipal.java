import java.util.ArrayList;
import java.util.List;

class PratoPrincipal extends ItemMenu {
    private List<String> ingredientes;
    private int tempoPreparo;
    private String categoria;

    public PratoPrincipal(String nome, float preco, String descricao,
                          List<String> ingredientes, int tempoPreparo, String categoria) {
        super(nome, preco, descricao);
        this.ingredientes = new ArrayList<>(ingredientes);
        this.tempoPreparo = tempoPreparo;
        this.categoria = categoria;
    }

    @Override
    public void exibirDetalhes() {
        System.out.println("=== PRATO PRINCIPAL ===");
        System.out.println("Nome: " + getNome());
        System.out.println("Descrição: " + getDescricao());
        System.out.println("Preço: R$" + getPreco());
        System.out.println("Categoria: " + categoria);
        System.out.println("Tempo de preparo: " + tempoPreparo + " minutos");
        System.out.println("Ingredientes:");
        for (String ingrediente : ingredientes) {
            System.out.println("- " + ingrediente);
        }
        System.out.println("=======================");
    }

    // Getters específicos
    public List<String> getIngredientes() {
        return new ArrayList<>(ingredientes);
    }

    public int getTempoPreparo() {
        return tempoPreparo;
    }

    public String getCategoria() {
        return categoria;
    }
}
