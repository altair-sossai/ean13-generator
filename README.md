# Gerador de código de barras EAN13 utilizando SVG

Dado uma sequência numérica de um código de barras **EAN13**, é gerado um arquivo *.svg* com o desenho do código de barras.

Leia mais sobre o padrão EAN13 no Wikipédia: [EAN-13](https://pt.wikipedia.org/wiki/EAN-13)


## Exemplo de uso

Dado a entrada **7891150027794** (Mostarda Hellmann's xd)
```csharp

var svg = Ean13Generator.Svg("7891150027794")

```

Produz o seguinte código **svg**:
```svg

<svg xmlns="http://www.w3.org/2000/svg">
    <line x1="0" y1="0" x2="190" y2="0" stroke="black" stroke-width="190"
        stroke-dasharray="2 2 2 2 4 2 6 4 2 2 6 4 4 4 2 2 4 4 4 2 4 6 2 2 2 4 6 2 2 2 2 2 6 4 2 2 4 2 4 4 2 6 2 4 2 6 2 4 6 2 2 4 2 2 6 4 2 2" />
</svg>

```

Renderização da imagem:

![Mostarda Hellmann's - 7891150027794](https://raw.githubusercontent.com/altair-sossai/ean13-generator/main/Samples/7891150027794.svg)
