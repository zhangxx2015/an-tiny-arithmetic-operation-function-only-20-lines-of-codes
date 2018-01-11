class Program {
    /// <summary>
    /// an tiny arithmetic-operation function by QQ:20437023 liaisonme@hotmail.com
    /// MIT License Copyright (c) 2017 zhangxx2015
    ///
    /// 一个四则运算函数,支持运算符优先级 仅 22 行代码, 
    /// 实现了:
    ///     加法(+),
    ///     减法(-),
    ///     乘法(*),
    ///     除法(/),
    ///     取余(%),
    /// 
    /// an arithmetic-operation function,support PRI., only 22 lines of code,
    /// Implement features:
    ///     addition      (+),
    ///     subduction    (-),
    ///     multiplication(*),
    //      division      (/),
    //      remainder     (%)
    /// </summary>
    private static double Eval(string express) {
        while (true) {
            var lb = express.LastIndexOf('(');
            if (lb == -1) break;
            var rb = express.IndexOf(')', lb);
            if (-1 == rb) throw new System.Exception("Symbol is not match!");
            express = express.Substring(0, lb) + Eval(express.Substring(1 + lb, rb - lb - 1)) + express.Substring(rb + 1);
        }
        double ret;
        if (double.TryParse(express, out ret)) return ret;
        var op = express.IndexOf('+');
        if (-1 != op) return Eval(express.Substring(0, op)) + Eval(express.Substring(1 + op));
        op = express.IndexOf('-');
        if (-1 != op) return Eval(express.Substring(0, op)) - Eval(express.Substring(1 + op));
        op = express.IndexOf('%');
        if (-1 != op) return Eval(express.Substring(0, op)) % Eval(express.Substring(1 + op));
        op = express.IndexOf('*');
        if (-1 != op) return Eval(express.Substring(0, op)) * Eval(express.Substring(1 + op));
        op = express.IndexOf('/');
        if (-1 != op) return Eval(express.Substring(0, op)) / Eval(express.Substring(1 + op));
        throw new System.Exception("Unknow error!");
    }
    // Examples
    static void Main() {
        var exp1 = -11 + (1.2 + 3.4)*5.6/((7.8 + 9) - 10);
        var exp2 = Eval("-11 + (1.2 + 3.4) * 5.6 / ((7.8 + 9) - 10)");
        System.Console.WriteLine("-11 + (1.2 + 3.4) * 5.6 / ((7.8 + 9) - 10) = {0},validate:{1}", exp1, exp1 == exp2);
        // benchmark
        var myTicks = System.Environment.TickCount;
        for (var i = 0; i < 1000000; i++) {
            var ret = Eval("-11 + (1.2 + 3.4) * 5.6 / ((7.8 + 9) - 10)");
        }
        myTicks = System.Environment.TickCount - myTicks;

        var sysTicks = System.Environment.TickCount;
        for (var i = 0; i < 1000000; i++) {
            var ret = -11 + (1.2 + 3.4) * 5.6 / ((7.8 + 9) - 10);
        }
        sysTicks = System.Environment.TickCount - sysTicks;
        
        System.Console.WriteLine("execute 1,000,000 times,eval function elapse:{0} ms,native code elapse:{1} ms", myTicks, sysTicks);
        System.Console.ReadLine();
    }
}
