import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

class Pedido {
    private static int proximoNumero = 1;

    private int numeroPedido;
    private List<ItemMenu> itens;
    private LocalDateTime data;

    public Pedido() {
        this.numeroPedido = proximoNumero++;
        this.itens = new ArrayList<>();
        this.data = LocalDateTime.now();
    }

    public void adicionarItem(ItemMenu item) {
        itens.add(item);
        System.out.println("Item adicionado ao pedido #" + numeroPedido + ":");
        item.exibirDetalhes();
    }

    public float calcularTotal() {
        float total = 0;
        for (ItemMenu item : itens) {
            total += item.getPreco();
        }
        return total;
    }

    public void exibirPedido() {
        System.out.println("\n=== PEDIDO #" + numeroPedido + " ===");
        System.out.println("Data: " + data.format(DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm")));
        System.out.println("Itens:");

        for (ItemMenu item : itens) {
            System.out.println("- " + item.getNome() + ": R$" + item.getPreco());
        }

        System.out.println("Total: R$" + calcularTotal());
        System.out.println("====================\n");
    }

    // Getters
    public int getNumeroPedido() {
        return numeroPedido;
    }

    public List<ItemMenu> getItens() {
        return new ArrayList<>(itens);
    }

    public LocalDateTime getData() {
        return data;
    }
}
