--
-- PostgreSQL database dump
--

-- Dumped from database version 15.4
-- Dumped by pg_dump version 15.4

-- Started on 2025-12-07 10:27:04

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 2351781)
-- Name: category; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.category (
    category_id integer NOT NULL,
    name character varying(50) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    created_date timestamp without time zone DEFAULT now() NOT NULL,
    updated_date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.category OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 2351780)
-- Name: category_category_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.category_category_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.category_category_id_seq OWNER TO postgres;

--
-- TOC entry 3413 (class 0 OID 0)
-- Dependencies: 214
-- Name: category_category_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.category_category_id_seq OWNED BY public.category.category_id;


--
-- TOC entry 221 (class 1259 OID 2351816)
-- Name: discount_rule; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.discount_rule (
    discount_rule_id integer NOT NULL,
    name character varying(150) NOT NULL,
    discount_percent integer NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    created_date timestamp without time zone DEFAULT now() NOT NULL,
    updated_date timestamp without time zone DEFAULT now() NOT NULL,
    CONSTRAINT discount_rule_discount_percent_check CHECK (((discount_percent >= 0) AND (discount_percent <= 100)))
);


ALTER TABLE public.discount_rule OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 2351827)
-- Name: discount_rule_category; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.discount_rule_category (
    discount_rule_category_id integer NOT NULL,
    discount_rule_id integer NOT NULL,
    category_id integer NOT NULL
);


ALTER TABLE public.discount_rule_category OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 2351826)
-- Name: discount_rule_category_discount_rule_category_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.discount_rule_category_discount_rule_category_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.discount_rule_category_discount_rule_category_id_seq OWNER TO postgres;

--
-- TOC entry 3414 (class 0 OID 0)
-- Dependencies: 222
-- Name: discount_rule_category_discount_rule_category_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.discount_rule_category_discount_rule_category_id_seq OWNED BY public.discount_rule_category.discount_rule_category_id;


--
-- TOC entry 220 (class 1259 OID 2351815)
-- Name: discount_rule_discount_rule_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.discount_rule_discount_rule_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.discount_rule_discount_rule_id_seq OWNER TO postgres;

--
-- TOC entry 3415 (class 0 OID 0)
-- Dependencies: 220
-- Name: discount_rule_discount_rule_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.discount_rule_discount_rule_id_seq OWNED BY public.discount_rule.discount_rule_id;


--
-- TOC entry 219 (class 1259 OID 2351801)
-- Name: extra; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.extra (
    extra_id integer NOT NULL,
    category_id integer NOT NULL,
    name character varying(100) NOT NULL,
    price numeric(10,2) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    created_date timestamp without time zone DEFAULT now() NOT NULL,
    updated_date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.extra OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 2351800)
-- Name: extra_extra_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.extra_extra_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.extra_extra_id_seq OWNER TO postgres;

--
-- TOC entry 3416 (class 0 OID 0)
-- Dependencies: 218
-- Name: extra_extra_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.extra_extra_id_seq OWNED BY public.extra.extra_id;


--
-- TOC entry 225 (class 1259 OID 2351844)
-- Name: order; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."order" (
    order_id integer NOT NULL,
    sandwich_id integer NOT NULL,
    discount_rule_id integer,
    subtotal numeric(10,2) NOT NULL,
    total_price numeric(10,2) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    created_date timestamp without time zone DEFAULT now() NOT NULL,
    updated_date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."order" OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 2351864)
-- Name: order_item; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.order_item (
    order_item_id integer NOT NULL,
    order_id integer NOT NULL,
    extra_id integer NOT NULL,
    created_date timestamp without time zone DEFAULT now() NOT NULL,
    updated_date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.order_item OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 2351863)
-- Name: order_item_order_item_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.order_item_order_item_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.order_item_order_item_id_seq OWNER TO postgres;

--
-- TOC entry 3417 (class 0 OID 0)
-- Dependencies: 226
-- Name: order_item_order_item_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.order_item_order_item_id_seq OWNED BY public.order_item.order_item_id;


--
-- TOC entry 224 (class 1259 OID 2351843)
-- Name: order_order_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.order_order_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.order_order_id_seq OWNER TO postgres;

--
-- TOC entry 3418 (class 0 OID 0)
-- Dependencies: 224
-- Name: order_order_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.order_order_id_seq OWNED BY public."order".order_id;


--
-- TOC entry 217 (class 1259 OID 2351791)
-- Name: sandwich; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sandwich (
    sandwich_id integer NOT NULL,
    name character varying(100) NOT NULL,
    price numeric(10,2) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    created_date timestamp without time zone DEFAULT now() NOT NULL,
    updated_date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.sandwich OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 2351790)
-- Name: sandwich_sandwich_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.sandwich_sandwich_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.sandwich_sandwich_id_seq OWNER TO postgres;

--
-- TOC entry 3419 (class 0 OID 0)
-- Dependencies: 216
-- Name: sandwich_sandwich_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.sandwich_sandwich_id_seq OWNED BY public.sandwich.sandwich_id;


--
-- TOC entry 3203 (class 2604 OID 2351784)
-- Name: category category_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.category ALTER COLUMN category_id SET DEFAULT nextval('public.category_category_id_seq'::regclass);


--
-- TOC entry 3215 (class 2604 OID 2351819)
-- Name: discount_rule discount_rule_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discount_rule ALTER COLUMN discount_rule_id SET DEFAULT nextval('public.discount_rule_discount_rule_id_seq'::regclass);


--
-- TOC entry 3219 (class 2604 OID 2351830)
-- Name: discount_rule_category discount_rule_category_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discount_rule_category ALTER COLUMN discount_rule_category_id SET DEFAULT nextval('public.discount_rule_category_discount_rule_category_id_seq'::regclass);


--
-- TOC entry 3211 (class 2604 OID 2351804)
-- Name: extra extra_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.extra ALTER COLUMN extra_id SET DEFAULT nextval('public.extra_extra_id_seq'::regclass);


--
-- TOC entry 3220 (class 2604 OID 2351847)
-- Name: order order_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."order" ALTER COLUMN order_id SET DEFAULT nextval('public.order_order_id_seq'::regclass);


--
-- TOC entry 3224 (class 2604 OID 2351867)
-- Name: order_item order_item_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_item ALTER COLUMN order_item_id SET DEFAULT nextval('public.order_item_order_item_id_seq'::regclass);


--
-- TOC entry 3207 (class 2604 OID 2351794)
-- Name: sandwich sandwich_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sandwich ALTER COLUMN sandwich_id SET DEFAULT nextval('public.sandwich_sandwich_id_seq'::regclass);


--
-- TOC entry 3395 (class 0 OID 2351781)
-- Dependencies: 215
-- Data for Name: category; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.category (category_id, name, is_active, created_date, updated_date) FROM stdin;
1	Fries	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
2	Drink	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
\.


--
-- TOC entry 3401 (class 0 OID 2351816)
-- Dependencies: 221
-- Data for Name: discount_rule; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.discount_rule (discount_rule_id, name, discount_percent, is_active, created_date, updated_date) FROM stdin;
1	Sandwich + Fries + Drink	20	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
2	Sandwich + Drink	15	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
3	Sandwich + Fries	10	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
\.


--
-- TOC entry 3403 (class 0 OID 2351827)
-- Dependencies: 223
-- Data for Name: discount_rule_category; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.discount_rule_category (discount_rule_category_id, discount_rule_id, category_id) FROM stdin;
1	1	1
2	1	2
3	2	2
4	3	1
\.


--
-- TOC entry 3399 (class 0 OID 2351801)
-- Dependencies: 219
-- Data for Name: extra; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.extra (extra_id, category_id, name, price, is_active, created_date, updated_date) FROM stdin;
1	1	Fries	2.00	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
2	2	Soft Drink	2.50	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
\.


--
-- TOC entry 3405 (class 0 OID 2351844)
-- Dependencies: 225
-- Data for Name: order; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."order" (order_id, sandwich_id, discount_rule_id, subtotal, total_price, is_active, created_date, updated_date) FROM stdin;
1	1	1	9.50	7.60	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
\.


--
-- TOC entry 3407 (class 0 OID 2351864)
-- Dependencies: 227
-- Data for Name: order_item; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.order_item (order_item_id, order_id, extra_id, created_date, updated_date) FROM stdin;
1	1	1	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
2	1	2	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
\.


--
-- TOC entry 3397 (class 0 OID 2351791)
-- Dependencies: 217
-- Data for Name: sandwich; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sandwich (sandwich_id, name, price, is_active, created_date, updated_date) FROM stdin;
1	Burger	5.00	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
2	Egg	4.50	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
3	Bacon	7.00	t	2025-12-07 09:49:05.158259	2025-12-07 09:49:05.158259
\.


--
-- TOC entry 3420 (class 0 OID 0)
-- Dependencies: 214
-- Name: category_category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.category_category_id_seq', 2, true);


--
-- TOC entry 3421 (class 0 OID 0)
-- Dependencies: 222
-- Name: discount_rule_category_discount_rule_category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.discount_rule_category_discount_rule_category_id_seq', 4, true);


--
-- TOC entry 3422 (class 0 OID 0)
-- Dependencies: 220
-- Name: discount_rule_discount_rule_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.discount_rule_discount_rule_id_seq', 3, true);


--
-- TOC entry 3423 (class 0 OID 0)
-- Dependencies: 218
-- Name: extra_extra_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.extra_extra_id_seq', 2, true);


--
-- TOC entry 3424 (class 0 OID 0)
-- Dependencies: 226
-- Name: order_item_order_item_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.order_item_order_item_id_seq', 2, true);


--
-- TOC entry 3425 (class 0 OID 0)
-- Dependencies: 224
-- Name: order_order_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.order_order_id_seq', 1, true);


--
-- TOC entry 3426 (class 0 OID 0)
-- Dependencies: 216
-- Name: sandwich_sandwich_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.sandwich_sandwich_id_seq', 3, true);


--
-- TOC entry 3229 (class 2606 OID 2351789)
-- Name: category category_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.category
    ADD CONSTRAINT category_pkey PRIMARY KEY (category_id);


--
-- TOC entry 3239 (class 2606 OID 2351832)
-- Name: discount_rule_category discount_rule_category_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discount_rule_category
    ADD CONSTRAINT discount_rule_category_pkey PRIMARY KEY (discount_rule_category_id);


--
-- TOC entry 3236 (class 2606 OID 2351825)
-- Name: discount_rule discount_rule_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discount_rule
    ADD CONSTRAINT discount_rule_pkey PRIMARY KEY (discount_rule_id);


--
-- TOC entry 3233 (class 2606 OID 2351809)
-- Name: extra extra_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.extra
    ADD CONSTRAINT extra_pkey PRIMARY KEY (extra_id);


--
-- TOC entry 3244 (class 2606 OID 2351871)
-- Name: order_item order_item_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_item
    ADD CONSTRAINT order_item_pkey PRIMARY KEY (order_item_id);


--
-- TOC entry 3241 (class 2606 OID 2351852)
-- Name: order order_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."order"
    ADD CONSTRAINT order_pkey PRIMARY KEY (order_id);


--
-- TOC entry 3231 (class 2606 OID 2351799)
-- Name: sandwich sandwich_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sandwich
    ADD CONSTRAINT sandwich_pkey PRIMARY KEY (sandwich_id);


--
-- TOC entry 3237 (class 1259 OID 2351884)
-- Name: idx_discount_rule_active; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_discount_rule_active ON public.discount_rule USING btree (is_active);


--
-- TOC entry 3234 (class 1259 OID 2351882)
-- Name: idx_extra_category; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_extra_category ON public.extra USING btree (category_id);


--
-- TOC entry 3242 (class 1259 OID 2351883)
-- Name: idx_orderitem_order; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_orderitem_order ON public.order_item USING btree (order_id);


--
-- TOC entry 3246 (class 2606 OID 2351838)
-- Name: discount_rule_category discount_rule_category_category_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discount_rule_category
    ADD CONSTRAINT discount_rule_category_category_id_fkey FOREIGN KEY (category_id) REFERENCES public.category(category_id) ON DELETE RESTRICT;


--
-- TOC entry 3247 (class 2606 OID 2351833)
-- Name: discount_rule_category discount_rule_category_discount_rule_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discount_rule_category
    ADD CONSTRAINT discount_rule_category_discount_rule_id_fkey FOREIGN KEY (discount_rule_id) REFERENCES public.discount_rule(discount_rule_id) ON DELETE CASCADE;


--
-- TOC entry 3245 (class 2606 OID 2351810)
-- Name: extra extra_category_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.extra
    ADD CONSTRAINT extra_category_id_fkey FOREIGN KEY (category_id) REFERENCES public.category(category_id) ON DELETE RESTRICT;


--
-- TOC entry 3248 (class 2606 OID 2351858)
-- Name: order order_discount_rule_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."order"
    ADD CONSTRAINT order_discount_rule_id_fkey FOREIGN KEY (discount_rule_id) REFERENCES public.discount_rule(discount_rule_id);


--
-- TOC entry 3250 (class 2606 OID 2351877)
-- Name: order_item order_item_extra_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_item
    ADD CONSTRAINT order_item_extra_id_fkey FOREIGN KEY (extra_id) REFERENCES public.extra(extra_id) ON DELETE RESTRICT;


--
-- TOC entry 3251 (class 2606 OID 2351872)
-- Name: order_item order_item_order_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_item
    ADD CONSTRAINT order_item_order_id_fkey FOREIGN KEY (order_id) REFERENCES public."order"(order_id) ON DELETE CASCADE;


--
-- TOC entry 3249 (class 2606 OID 2351853)
-- Name: order order_sandwich_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."order"
    ADD CONSTRAINT order_sandwich_id_fkey FOREIGN KEY (sandwich_id) REFERENCES public.sandwich(sandwich_id);


-- Completed on 2025-12-07 10:27:05

--
-- PostgreSQL database dump complete
--

