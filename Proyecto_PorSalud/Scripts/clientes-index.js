var ClientesIndex = (function () {
    var state = { page: 1, pageSize: 20, search: "" };

    async function cargar() {
        //$("#contenedorTabla").load("/Clientes/Lista", state);
        //$("#contenedorTabla").load("/Clientes/Lista" + '?' + $.param({ page: state.page, pageSize: state.pageSize, search: state.search }));
        const url = "/Clientes/Lista";
        const params = new URLSearchParams({ page: state.page, pageSize: state.pageSize, search: state.search });
        const resp = await fetch(`${url}?${params}`);
        const html = await resp.text();
        //console.log(html);
        document.getElementById('contenedorTabla').innerHTML = html;

    }

    function wire() {
        $("#btnBuscar").on("click", function () {
            state.search = $("#txtSearch").val();
            state.page = 1;
            cargar();
        });

        // delegaci�n para links de paginado
        $("#contenedorTabla").on("click", ".link-page", function (e) {
            e.preventDefault();
            console.log(e)
            state.page = $(this).data("page");
            cargar();
        });

        // cambiar tama�o de p�gina
        $(document).on("change.clientes", "#pageSize[data-page-size]", function () {
            var ps = parseInt($(this).val(), 10);
            if (!isNaN(ps) && ps > 0) {
                state.pageSize = ps;  // usa tu estado existente
                state.page = 1;       // reinicia a la primera p�gina
                cargar();
            }
        });

    }

    return {
        init: function () { wire(); cargar(); }
    };
})();
